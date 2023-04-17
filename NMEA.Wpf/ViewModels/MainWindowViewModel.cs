using System;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using AutoMapper;
using MaterialDesignThemes.Wpf;
using NMEA.Wpf.Common;
using NMEA.Wpf.Common.Navigation;
using NMEA.Wpf.Extensions;
using NMEA.Wpf.Models;
using NMEA.Wpf.Services;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace NMEA.Wpf.ViewModels
{
    public class MainWindowViewModel : NavigationBase, IInitializeConfigure
    {
        // 区域管理器
        private readonly IRegionManager regions;
        private readonly IMapper mapper;
        private readonly IDialogHostService dialog;
        private readonly ISettingsService settingsService;

        private String title;
        public String Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        // 菜单栏
        private ObservableCollection<MenuBar> menus;
        public ObservableCollection<MenuBar> Menus
        {
            get { return menus; }
            set { SetProperty(ref menus, value); }
        }

        public DelegateCommand<MenuBar> NavigationCommand { get; private set; }
        public MainWindowViewModel(IContainerProvider provider) : base(provider)
        {
            regions = provider.Resolve<IRegionManager>();
            mapper = provider.Resolve<IMapper>();
            dialog = provider.Resolve<IDialogHostService>();
            settingsService = provider.Resolve<ISettingsService>();

            Title = PrismManager.AppName;
            Menus = new ObservableCollection<MenuBar>();

            // 导航跳转
            NavigationCommand = new DelegateCommand<MenuBar>(bar => {
                if (bar == null || String.IsNullOrEmpty(bar.Navigation)) return;
                regions.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.Navigation);
            });
        }

        public void InitializeConfigure()
        {
            SkinExtensions.ModifyTheme(theme => theme.SetBaseTheme(settingsService.Settings.Skin.IsDarkTheme ? Theme.Dark : Theme.Light));
            SkinExtensions.ChangePrimaryHue(settingsService.Settings.Skin.PrimaryHue);

            Menus.Add(new MenuBar { Icon = "\ue65e", Name = "首页", Navigation = "IndexView" });
            Menus.Add(new MenuBar { Icon = "\ue634", Name = "设置", Navigation = "SettingsView" });

            // 默认主页
            regions.Regions[PrismManager.MainViewRegionName].RequestNavigate("IndexView");
        }
    }
}
