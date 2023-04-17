using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using NMEA.Wpf.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace NMEA.Wpf.ViewModels.Dialogs
{
    public class MessageViewModel : BindableBase, IDialogHostAware
    {
        private String _title;
        public String Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private String _content;
        public String Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public String DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<String>("Title");
            Content = parameters.GetValue<String>("Content");
        }

        public MessageViewModel()
        {
            SaveCommand = new DelegateCommand(() => {
                if (DialogHost.IsDialogOpen(DialogHostName))
                {
                    DialogParameters pm = new DialogParameters();
                    DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.Yes, pm));
                }
            });

            CancelCommand = new DelegateCommand(() => {
                if (DialogHost.IsDialogOpen(DialogHostName))
                    DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
            });
        }
    }
}
