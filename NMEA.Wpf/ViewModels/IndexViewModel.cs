using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DryIoc;
using Newtonsoft.Json;
using NMEA.Wpf.Common.Events;
using NMEA.Wpf.Common.Helpers;
using NMEA.Wpf.Common.Navigation;
using NMEA.Wpf.Extensions;
using NMEA.Wpf.Models;
using NMEA.Wpf.Services;
using NmeaParser;
using NmeaParser.Messages;
using NmeaParser.Messages.Garmin;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;

namespace NMEA.Wpf.ViewModels
{
    public class IndexViewModel : NavigationBase
    {
        private Timer timer = new Timer();
        public IGlobalService Global { get; set; }
        private readonly ISettingsService settingsService;

        private AtHelper helper;
        public AtHelper Helper
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

        private ObservableCollection<SerialMessage> serialMessages;
        public ObservableCollection<SerialMessage> SerialMessages
        {
            get { return serialMessages; }
            set { SetProperty(ref serialMessages, value); }
        }

        public AtTestSettings AtTest { get; set; }
        public DelegateCommand OpenCommand { get; private set; }
        public DelegateCommand TestCommand { get; private set; }

        public IndexViewModel(IContainerProvider provider) : base(provider)
        {
            SerialMessages = new ObservableCollection<SerialMessage>();

            Global = provider.Resolve<IGlobalService>();
            settingsService = provider.Resolve<ISettingsService>();

            IsEnable = true;
            BtnOpenText = "打开";

            Helper = new AtHelper((s, e) => {
                App.Current.Dispatcher.BeginInvoke(() => {
                    SerialMessages.Add(e.Message);
                    ScrollbarToEnd();
                });
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

            OpenCommand = new DelegateCommand(() => {
                // Message(Helper.BaudRate + Helper.PortName);
                try
                {
                    if (Helper.IsOpen)
                    {
                        Helper.Close();
                    }
                    else
                    {
                        Helper.Open();
                    }
                }
                catch (Exception ex)
                {
                    Message(ex.Message);
                }
            });

            TestCommand = new DelegateCommand(async () => {
                await Auto();
            });
        }

        public Task Auto()
        {
            return Task.Run(() => {
                try
                {
                    Int32 step = 1;
                    Int32? cnt;
Retry:
                    if (!AtTest.Lists.ContainsKey(step)) throw new Exception($"Key = {step} 数据不存在");
                    var at = AtTest.Lists[step];
                    cnt = at.Retry;
                    while (true)
                    {
                        if (Helper.CheckCommand(at.Command, at.Return, at.Timeout)) break;

                        if (cnt == null || cnt <= 0) throw new Exception($"{at.Command} 失败");
                        if (cnt != null) cnt--;
                    }

                    // 执行完成
                    if (at.Next == null || at.Next == 0)
                    {
                        Message("Auto Done.");
                        return;
                    }
                    step = (Int32)at.Next;
                    goto Retry;
                }
                catch (Exception ex)
                {
                    Message(ex.Message);
                }
            });
        }

        private void ScrollbarToEnd()
        {
            App.Current.Dispatcher.Invoke(() => {
                aggregator.SetSerialMessage(new SerialMessageModel { IsSerialMessageToEnd = true });
            });
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (settingsService != null)
                AtTest = settingsService.Settings.AtTest;
            base.OnNavigatedTo(navigationContext);
        }
    }
}
