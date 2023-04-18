using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NmeaParser.Messages;

namespace NMEA.Wpf.Controls
{
    /// <summary>
    /// VtgControl.xaml 的交互逻辑
    /// </summary>
    public partial class VtgControl : UserControl
    {
        public VtgControl()
        {
            InitializeComponent();
        }

        public Vtg Message
        {
            get { return (Vtg)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(Vtg), typeof(VtgControl), new PropertyMetadata(null));
    }
}
