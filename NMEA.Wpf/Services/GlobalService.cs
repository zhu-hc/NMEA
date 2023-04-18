using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA.Wpf.Models;

namespace NMEA.Wpf.Services
{
    public interface IGlobalService
    {
        NmeaMessage NMEA { get; }
    }

    public class GlobalService : IGlobalService
    {
        private NmeaMessage nmea = new NmeaMessage();
        public NmeaMessage NMEA
        {
            get { return nmea; }
        }
    }
}
