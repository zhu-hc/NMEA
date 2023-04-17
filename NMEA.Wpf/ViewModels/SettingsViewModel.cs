using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using NMEA.Wpf.Common.Navigation;
using NMEA.Wpf.Extensions;
using NMEA.Wpf.Services;
using Prism.Commands;
using Prism.Ioc;

namespace NMEA.Wpf.ViewModels
{
    public class SettingsViewModel : NavigationBase
    {
        // 设置
        private readonly ISettingsService settingsService;

        private Boolean _isDarkTheme;
        public Boolean IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    SkinExtensions.ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));

                    settingsService.Settings.Skin.IsDarkTheme = value;
                    settingsService.Save();
                }
            }
        }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public DelegateCommand<Object> ChangeHueCommand { get; private set; }
        public SettingsViewModel(IContainerProvider provider) : base(provider)
        {
            settingsService = provider.Resolve<ISettingsService>();

            IsDarkTheme = settingsService.Settings.Skin.IsDarkTheme;
            ChangeHueCommand = new DelegateCommand<Object>(obj => {
                var color = (Color)obj;
                SkinExtensions.ChangePrimaryHue(color);
                settingsService.Settings.Skin.PrimaryHue = color;
                settingsService.Save();
            });
        }
    }
}
