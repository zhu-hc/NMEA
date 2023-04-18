using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Timers;
using System.Windows;
using DryIoc;
using Newtonsoft.Json;
using NMEA.Wpf.Common.Helpers;
using NMEA.Wpf.Common.Navigation;
using NMEA.Wpf.Services;
using NmeaParser;
using NmeaParser.Messages;
using NmeaParser.Messages.Garmin;
using Prism.Commands;
using Prism.Ioc;

namespace NMEA.Wpf.ViewModels
{
    public class NmeaViewModel : GeneralBase
    {
        private Timer timer = new Timer();
        public IGlobalService Global { get; set; }

        private NmeaHelper helper;
        public NmeaHelper Helper
        {
            get { return helper; }
            set { SetProperty(ref helper, value); }
        }

        private Boolean isEnable;
        public Boolean IsEnable
        {
            get { return isEnable; }
            set { SetProperty(ref isEnable, value); }
        }

        private String btnOpenText;
        public String BtnOpenText
        {
            get { return btnOpenText; }
            set { SetProperty(ref btnOpenText, value); }
        }

        public DelegateCommand OpenCommand { get; private set; }
        public NmeaViewModel(IContainerProvider provider) : base(provider)
        {
            Global = provider.Resolve<IGlobalService>();

            IsEnable = true;
            BtnOpenText = "打开";

            Helper = new NmeaHelper((s, e) => {
                if (e.Message is Rmc rmc)
                    Global.NMEA.Rmc = rmc;
                else if (e.Message is Gga gga)
                    Global.NMEA.Gga = gga;
                else if (e.Message is Gsa gsa)
                    Global.NMEA.Gsa = gsa;
                else if (e.Message is Gll gll)
                    Global.NMEA.Gll = gll;
                else if (e.Message is Pgrme pgrme)
                    Global.NMEA.Pgrme = pgrme;
                else if (e.Message is Vtg vtg)
                    Global.NMEA.Vtg = vtg;
                else
                {
                    // Message(e.Message.ToString());
                }
            });

            Helper.BaudRate = "115200";

            // Timer
            timer.Elapsed += new ElapsedEventHandler((s, e) => {
                if (IsEnable == Helper.IsOpen)
                {
                    IsEnable = !Helper.IsOpen;
                    if (IsEnable) BtnOpenText = "打开";
                    else BtnOpenText = "关闭";
                }
            });
            timer.Interval = 100;
            timer.Enabled = true;

            OpenCommand = new DelegateCommand(async () => {
                // Message(Helper.BaudRate + Helper.PortName);
                try
                {
                    if (Helper.IsOpen)
                    {
                        await Helper.CloseAsync();
                    }
                    else
                    {
                        await Helper.OpenAsync();
                    }
                }
                catch (Exception ex)
                {
                    Message(ex.Message);
                }
            });
        }
    }
}
