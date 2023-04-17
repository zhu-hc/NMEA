using System.Threading.Tasks;
using System.Threading;
using System;
using System.Windows;
using AutoMapper;
using NMEA.Wpf.Common;
using NMEA.Wpf.Services;
using NMEA.Wpf.View.Dialogs;
using NMEA.Wpf.ViewModels;
using NMEA.Wpf.ViewModels.Dialogs;
using NMEA.Wpf.Views;
using Prism.Ioc;
using Serilog;
using System.Windows.Threading;

namespace NMEA.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // 日志配置
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("Logs\\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("软件启动");

            // 单例运行
            _ = new Mutex(true, "NMEA.WPF", out bool ret);
            if (!ret)
            {
                Log.Warning("尝试打开多个软件");
                MessageBox.Show("程序已经打开");
                App.Current.Shutdown();
            }

            // 全局异常捕获
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            base.OnStartup(e);
        }

        // UI线程未捕获异常处理事件（UI主线程）
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            OnExceptionHandler(ex);

            e.Handled = true;//表示异常已处理，可以继续运行
        }

        // 非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
        // 如果UI线程异常DispatcherUnhandledException未注册，则如果发生了UI线程未处理异常也会触发此异常事件
        // 此机制的异常捕获后应用程序会直接终止。没有像DispatcherUnhandledException事件中的Handler=true的处理方式，可以通过比如Dispatcher.Invoke将子线程异常丢在UI主线程异常处理机制中处理
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                OnExceptionHandler(ex);
            }
        }

        // Task线程内未捕获异常处理事件
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            OnExceptionHandler(ex);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("软件关闭");
            Log.CloseAndFlush();
        }

        //异常处理 封装
        private void OnExceptionHandler(Exception ex)
        {
            if (ex != null)
            {
                string errorMsg = "";
                if (ex.InnerException != null)
                {
                    errorMsg += String.Format("【InnerException】{0}\n{1}\n", ex.InnerException.Message, ex.InnerException.StackTrace);
                }
                errorMsg += String.Format("{0}\n{1}", ex.Message, ex.StackTrace);

                Log.Error(errorMsg);
            }
        }

        protected override void OnInitialized()
        {
            // 初始化配置
            var init = App.Current.MainWindow.DataContext as IInitializeConfigure;
            init?.InitializeConfigure();

            base.OnInitialized();
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注册AutoMapper
            containerRegistry.Register<IAutoMapperService, AutoMapperService>();
            containerRegistry.Register(typeof(IMapper), container => {
                var provider = container.Resolve<IAutoMapperService>();
                return provider.GetMapper();
            });

            // 注册设置
            var settings = new SettingsService();
            settings.Read();
            containerRegistry.RegisterInstance(typeof(ISettingsService), settings);

            // 注册服务
            containerRegistry.RegisterSingleton<IDialogHostService, DialogHostService>();

            // 注册弹窗
            containerRegistry.RegisterForNavigation<MessageView, MessageViewModel>();

            // 注册导航
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<NmeaView, NmeaViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
    }
}
