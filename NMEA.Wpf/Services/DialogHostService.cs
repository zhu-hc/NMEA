using MaterialDesignThemes.Wpf;
using NMEA.Wpf.Common;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NMEA.Wpf.Services
{
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialogAsync(String name, IDialogParameters parameters, String host = "Root");
    }

    /// <summary>
    /// 对话框主机服务
    /// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension container;
        public DialogHostService(IContainerExtension container) : base(container)
        {
            this.container = container;
        }

        public async Task<IDialogResult> ShowDialogAsync(string name, IDialogParameters parameters, string host = "Root")
        {
            if (parameters == null) parameters = new DialogParameters();

            // 从容器中取出弹出窗口的实例
            var content = container.Resolve<object>(name);
            // 验证实例的有效性
            if (content is not FrameworkElement dialogContent)
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            if (dialogContent.DataContext is null && ViewModelLocator.GetAutoWireViewModel(dialogContent) is null)
                ViewModelLocator.SetAutoWireViewModel(dialogContent, true);

            if (dialogContent.DataContext is not IDialogHostAware viewmodel)
                throw new NullReferenceException("A dialog's viewmodel must be a IDialogHostAware");

            viewmodel.DialogHostName = host;
            DialogOpenedEventHandler eventHandler = (sender, args) =>
            {
                viewmodel.OnDialogOpened(parameters);
                args.Session.UpdateContent(content);
            };

            return (IDialogResult)await DialogHost.Show(dialogContent, host, eventHandler);
        }
    }
}
