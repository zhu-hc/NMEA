using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using NMEA.Wpf.Controls;
using NMEA.Wpf.Models;
using NmeaParser;

namespace NMEA.Wpf.Common.Converters
{
    public class SnrToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SatelliteInfo sv)
            {
                if (parameter as string == "Name")
                {
                    string name;
                    if (sv.TalkerId == Talker.GlobalPositioningSystem)
                        name = "GPS";
                    else if (sv.TalkerId == Talker.GlonassReceiver)
                        name = "GLONASS";
                    else if (sv.TalkerId == Talker.BeiDouNavigationSatelliteSystem)
                        name = "BeiDou";
                    else if (sv.TalkerId == Talker.QuasiZenithSatelliteSystem)
                        name = "QZSS";
                    else if (sv.TalkerId == Talker.IndianRegionalNavigationSatelliteSystem)
                        name = "NavIC IRNSS";
                    else
                        name = sv.TalkerId.ToString();
                    var snrs = string.Join("\n", sv.SNRs.Select(snr => SatelliteSnr.SignalIdToName(snr.Key, sv.TalkerId) + ": " + snr.Value + "dB"));
                    if (!string.IsNullOrEmpty(snrs))
                        name += $"\n{snrs}";

                    return name;
                }
                else
                    return Math.Max(10, sv.SignalToNoiseRatio * 2);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
