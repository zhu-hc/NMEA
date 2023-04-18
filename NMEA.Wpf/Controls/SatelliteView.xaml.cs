using System;
using System.Collections.Generic;
using System.Globalization;
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
using NmeaParser;
using NmeaParser.Messages;

namespace NMEA.Wpf.Controls
{
    /// <summary>
    /// SatelliteView.xaml 的交互逻辑
    /// </summary>
    public partial class SatelliteView : UserControl
    {
        public SatelliteView()
        {
            InitializeComponent();
        }

        Dictionary<String, Gsv> messages = new Dictionary<String, Gsv>();
        public void SetGsv(Gsv message)
        {
            messages[message.TalkerId + "+" + message.GnssSignalId] = message;
            UpdateSatellites();
        }
        public void ClearGsv()
        {
            messages.Clear();
            UpdateSatellites();
        }

        private void UpdateSatellites()
        {
            satellites.ItemsSource = messages.Values.SelectMany(g => g.SVs);
        }
    }

    public class PolarPlacementItem : ContentControl
    {
        public PolarPlacementItem()
        {
            HorizontalAlignment =HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
        }
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            double az = (Azimuth - 90) / 180 * Math.PI;
            double e = (90 - Elevation) / 90;
            double X = Math.Cos(az) * e;
            double Y = Math.Sin(az) * e;
            X = arrangeBounds.Width * .5 * X;
            Y = arrangeBounds.Height * .5 * Y;
            RenderTransform = new TranslateTransform(X, Y);
            return base.ArrangeOverride(arrangeBounds);
        }

        public double Azimuth
        {
            get { return (double)GetValue(AzimuthProperty); }
            set { SetValue(AzimuthProperty, value); }
        }

        public static readonly DependencyProperty AzimuthProperty =
            DependencyProperty.Register("Azimuth", typeof(double), typeof(PolarPlacementItem), new PropertyMetadata(0d, OnAzimuthPropertyChanged));

        private static void OnAzimuthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PolarPlacementItem).InvalidateArrange();
        }

        public double Elevation
        {
            get { return (double)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }

        public static readonly DependencyProperty ElevationProperty =
            DependencyProperty.Register("Elevation", typeof(double), typeof(PolarPlacementItem), new PropertyMetadata(0d, OnElevationPropertyChanged));

        private static void OnElevationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PolarPlacementItem).InvalidateArrange();
        }
    }
}
