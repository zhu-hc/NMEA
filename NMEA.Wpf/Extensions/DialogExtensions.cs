using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA.Wpf.Common;
using NMEA.Wpf.Services;
using Prism.Services.Dialogs;

namespace NMEA.Wpf.Extensions
{
    public static class DialogExtensions
    {
        /// <summary>
        /// 询问窗口
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public static async Task<IDialogResult> QuestionAsync(this IDialogHostService dialog, String title, String content, String host = "Root")
        {
            DialogParameters pm = new DialogParameters
            {
                { "Title", title },
                { "Content", content }
            };

            return await dialog.ShowDialogAsync("MessageView", pm, host);
        }
    }
}
