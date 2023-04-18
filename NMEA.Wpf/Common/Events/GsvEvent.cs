using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NmeaParser.Messages;
using Prism.Events;

namespace NMEA.Wpf.Common.Events
{
    public class GsvModel
    {
        public Gsv Gsv { get; set; }
    }

    public class GsvEvent : PubSubEvent<GsvModel>
    {
    }
}
