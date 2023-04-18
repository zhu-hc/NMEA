using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NmeaParser;

namespace NMEA.Wpf.Models
{
    public struct SatelliteInfo
    {
        public int Id;
        public Talker TalkerId;
        public Dictionary<char, int> SNRs;
        public int SignalToNoiseRatio => SNRs.Max(s => s.Value);
    }
}
