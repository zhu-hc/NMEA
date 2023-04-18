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
using NMEA.Wpf.Models;
using NmeaParser;
using NmeaParser.Messages;

namespace NMEA.Wpf.Controls
{
    /// <summary>
    /// SatelliteSnr.xaml 的交互逻辑
    /// </summary>
    public partial class SatelliteSnr : UserControl
    {
        public SatelliteSnr()
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
            //satellites.ItemsSource = messages.Values.SelectMany(g => g.SVs).OrderBy(s=>s.GnssSignalId).OrderBy(s => s.Id).OrderByDescending(s => s.TalkerId);
            var sats = messages.Values.SelectMany(g => g.SVs).GroupBy(s => s.TalkerId.ToString() + s.Id);
            var infos = sats.Select(s => new SatelliteInfo()
            {
                Id = s.First().Id,
                TalkerId = s.First().TalkerId,
                SNRs = new Dictionary<char, int>(s.Select(s => new KeyValuePair<char, int>(s.GnssSignalId, s.SignalToNoiseRatio)), null)
            });
            satellites.ItemsSource = infos;
        }

        internal static string SignalIdToName(char signalId, Talker talkerId)
        {
            if (signalId != '0')
            {
                var talker = talkerId;
                if (talker == Talker.GlobalPositioningSystem)
                {
                    switch (signalId)
                    {
                        case '1': return "L1 C/A";
                        case '2': return "L1 P(Y)";
                        case '3': return "L1 M";
                        case '4': return "L2 P(Y)";
                        case '5': return "L2C-M";
                        case '6': return "L2C-L";
                        case '7': return "L5-I";
                        case '8': return "L5-Q";
                    }
                }
                else if (talker == Talker.GlonassReceiver)
                {
                    switch (signalId)
                    {
                        case '1': return "G1 C/A";
                        case '2': return "G1 P";
                        case '3': return "G2 C/A";
                        case '4': return "GLONASS (M) G2 P";
                    }
                }
                else if (talker == Talker.GalileoPositioningSystem)
                {
                    switch (signalId)
                    {
                        case '1': return "E5a";
                        case '2': return "E5b";
                        case '3': return "E5 a+b";
                        case '4': return "E6-A";
                        case '5': return "E6-BC";
                        case '6': return "L1-A";
                        case '7': return "L1-BC";
                    }
                }
                else if (talker == Talker.BeiDouNavigationSatelliteSystem)
                {
                    switch (signalId)
                    {
                        case '1': return "B1I";
                        case '2': return "B1Q";
                        case '3': return "B1C";
                        case '4': return "B1A";
                        case '5': return "B2-a";
                        case '6': return "B2-b";
                        case '7': return "B2 a+b";
                        case '8': return "B3I";
                        case '9': return "B3Q";
                        case 'A': return "B3A";
                        case 'B': return "B2I";
                        case 'C': return "B2Q";
                    }
                }
                else if (talker == Talker.QuasiZenithSatelliteSystem)
                {
                    switch (signalId)
                    {
                        case '1': return "L1 C/A";
                        case '2': return "L1C (D)";
                        case '3': return "L1C (P)";
                        case '4': return "LIS";
                        case '5': return "L2C-M";
                        case '6': return "L2C-L";
                        case '7': return "L5-I";
                        case '8': return "L5-Q";
                        case '9': return "L6D";
                        case 'A': return "L6E";
                    }
                }
                else if (talker == Talker.IndianRegionalNavigationSatelliteSystem)
                {
                    switch (signalId)
                    {
                        case '1': return "L5-SPS";
                        case '2': return "S-SPS<";
                        case '3': return "L5-RS";
                        case '4': return "S-RS";
                        case '5': return "L1-SPS";
                    }
                }
            }
            return string.Empty;
        }
    }
}
