using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace NMEA.Wpf.Extensions
{
    public static class SkinExtensions
    {
        private readonly static PaletteHelper paletteHelper = new PaletteHelper();

        public static void ModifyTheme(Action<ITheme> modificationAction)
        {
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }

        public static void ChangePrimaryHue(Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }
    }
}
