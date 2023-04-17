using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NMEA.Wpf.Models
{
    public class SkinSettings
    {
        public Boolean IsDarkTheme { get; set; } = false;
        public Color PrimaryHue { get; set; } = Color.FromRgb(0x67, 0x3a, 0xb7);
    }

    public class ProgramSettings
    {
        public SkinSettings Skin { get; set; } = new SkinSettings();
    }
}
