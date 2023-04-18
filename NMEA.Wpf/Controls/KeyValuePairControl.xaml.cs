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

namespace NMEA.Wpf.Controls
{
    /// <summary>
    /// KeyValuePairControl.xaml 的交互逻辑
    /// </summary>
    public partial class KeyValuePairControl : UserControl
    {
        public KeyValuePairControl()
        {
            InitializeComponent();
        }

        public String Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(KeyValuePairControl), new PropertyMetadata(null));



        public Object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(KeyValuePairControl), new PropertyMetadata(null));
    }
}
