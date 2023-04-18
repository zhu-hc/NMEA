using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NmeaParser.Messages;
using NmeaParser;
using System.Windows.Media;
using System.Windows.Data;
using NMEA.Wpf.Models;

namespace NMEA.Wpf.Common.Converters
{
    public class SatelliteVechicleColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Talker? talker = null;
            int snr = 0;
            if (value is SatelliteVehicle sv)
            {
                talker = sv.TalkerId;
                snr = sv.SignalToNoiseRatio;
            }
            else if (value is SatelliteInfo info)
            {
                talker = info.TalkerId;
                snr = info.SignalToNoiseRatio;
            }
            if (talker != null)
            {
                byte alpha = (byte)(snr <= 0 ? 80 : 255);
                switch (talker)
                {
                    case Talker.GlobalPositioningSystem: return Color.FromArgb(alpha, 255, 0, 0);
                    case Talker.GalileoPositioningSystem: return Color.FromArgb(alpha, 0, 255, 0);
                    case Talker.GlonassReceiver: return Color.FromArgb(alpha, 0, 0, 255);
                    case Talker.BeiDouNavigationSatelliteSystem: return Color.FromArgb(alpha, 0, 255, 255);
                    case Talker.GlobalNavigationSatelliteSystem: return Color.FromArgb(alpha, 0, 0, 0);
                    default: return Colors.CornflowerBlue;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
