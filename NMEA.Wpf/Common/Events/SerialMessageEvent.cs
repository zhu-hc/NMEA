using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace NMEA.Wpf.Common.Events
{
    public class SerialMessageModel
    {
        public Boolean IsSerialMessageToEnd { get; set; }
    }

    public class SerialMessageEvent : PubSubEvent<SerialMessageModel>
    {
    }
}
