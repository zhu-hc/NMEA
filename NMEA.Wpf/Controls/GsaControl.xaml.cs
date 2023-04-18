﻿using System;
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
    /// GsaControl.xaml 的交互逻辑
    /// </summary>
    public partial class GsaControl : UserControl
    {
        public GsaControl()
        {
            InitializeComponent();
        }

        public Gsa Message
        {
            get { return (Gsa)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(Gsa), typeof(GsaControl), new PropertyMetadata(null, (d, e) => ((GsaControl)d).OnGsaPropertyChanged(e)));

        private void OnGsaPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Message == null)
                vehicles.Value = null;
            else
                vehicles.Value = string.Join(",", Message?.SatelliteIDs);
        }
    }
}
