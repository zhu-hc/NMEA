using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace NMEA.Wpf.Common.Events
{
    public class LoadingModel
    {
        public Boolean IsOpen { get; set; }
    }

    public class LoadingEvent : PubSubEvent<LoadingModel>
    {
    }
}
