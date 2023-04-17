using System.Windows;
using System.Windows.Input;
using NMEA.Wpf.Common;
using NMEA.Wpf.Extensions;
using NMEA.Wpf.Services;
using NMEA.Wpf.View.Dialogs;
using Prism.Events;
using Prism.Services.Dialogs;

namespace NMEA.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IEventAggregator aggregator, IDialogHostService dialog)
        {
            InitializeComponent();

            // 注册等待消息窗口
            aggregator.Register(arg => {
                DialogHost.IsOpen = arg.IsOpen;
                if (DialogHost.IsOpen) DialogHost.DialogContent = new ProgressView();
            });

            // 注册消息事件
            aggregator.Register(arg => {
                this.Snackbar.MessageQueue.Enqueue(arg.Message);
            });

            // 窗口移动
            ColorZone.MouseMove += (s, e) => {
                if (e.LeftButton == MouseButtonState.Pressed) this.DragMove();
            };

            // 窗口最小化
            btnMin.Click += (s, e) => {
                this.WindowState = WindowState.Minimized;
            };

            // 窗口最大化
            btnMax.Click += (s, e) => {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            };

            Window.StateChanged += (s, e) => {
                if (this.WindowState == WindowState.Maximized)
                    this.Dock.Margin = new Thickness(6);
                else
                    this.Dock.Margin = new Thickness(0);
            };

            // 退出软件
            btnClose.Click += async (s, e) => {
                var dialogResult = await dialog.QuestionAsync("温馨提示", $"确认退出系统？");
                if (dialogResult.Result != ButtonResult.Yes) return;

                this.Close();
                // Environment.Exit(0);
            };
        }
    }
}
