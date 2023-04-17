using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace NMEA.Wpf.Views
{
    /// <summary>
    /// NmeaView.xaml 的交互逻辑
    /// </summary>
    public partial class NmeaView : UserControl
    {
        public NmeaView()
        {
            InitializeComponent();
        }

        private void CbPorts_DropDownOpened(Object sender, EventArgs e)
        {
            if (sender != null && sender is ComboBox comboBox)
            {
                String[] list = SerialPort.GetPortNames();
                //添加串口项目  
                comboBox.Items.Clear();
                foreach (String s in list)
                {
                    //获取有多少个COM口  
                    comboBox.Items.Add(s);
                }
            }
        }
    }
}
