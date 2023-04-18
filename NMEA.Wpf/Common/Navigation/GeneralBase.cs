using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA.Wpf.Common.Events;
using NMEA.Wpf.Extensions;
using NmeaParser.Messages;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;

namespace NMEA.Wpf.Common.Navigation
{
    public class GeneralBase : BindableBase
    {
        protected readonly IEventAggregator aggregator;

        public GeneralBase(IContainerProvider containerProvider)
        {
            aggregator = containerProvider.Resolve<IEventAggregator>();
        }

        public void Message(String message)
        {
            App.Current.Dispatcher.Invoke(() => {
                aggregator.Message(new MessageModel { Message = message });
            });
        }

        public void Loading(Boolean isOpen)
        {
            App.Current.Dispatcher.Invoke(() => {
                aggregator.UpdateLoading(new LoadingModel { IsOpen = isOpen });
            });
        }
    }
}
