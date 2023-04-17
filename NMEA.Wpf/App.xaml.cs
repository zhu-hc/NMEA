using System.Windows;
using AutoMapper;
using NMEA.Wpf.Common;
using NMEA.Wpf.Services;
using NMEA.Wpf.View.Dialogs;
using NMEA.Wpf.ViewModels;
using NMEA.Wpf.ViewModels.Dialogs;
using NMEA.Wpf.Views;
using Prism.Ioc;

namespace NMEA.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnInitialized()
        {
            // 初始化配置
            var init = App.Current.MainWindow.DataContext as IInitializeConfigure;
            init?.InitializeConfigure();

            base.OnInitialized();
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册AutoMapper
            containerRegistry.Register<IAutoMapperService, AutoMapperService>();
            containerRegistry.Register(typeof(IMapper), container => {
                var provider = container.Resolve<IAutoMapperService>();
                return provider.GetMapper();
            });

            // 注册设置
            var settings = new SettingsService();
            settings.Read();
            containerRegistry.RegisterInstance(typeof(ISettingsService), settings);

            // 注册服务
            containerRegistry.RegisterSingleton<IDialogHostService, DialogHostService>();

            // 注册弹窗
            containerRegistry.RegisterForNavigation<MessageView, MessageViewModel>();

            // 注册导航
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
    }
}
