﻿using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA.Wpf.Common
{
    public interface IDialogHostAware
    {
        String DialogHostName { get; set; }
        void OnDialogOpened(IDialogParameters parameters);

        DelegateCommand SaveCommand { get; set; }
        DelegateCommand CancelCommand { get; set; }
    }
}
