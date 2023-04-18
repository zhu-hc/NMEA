using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA.Wpf.Models;
using Prism.Mvvm;
using Serilog;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace NMEA.Wpf.Common.Helpers
{
    public class AtMessageEventArgs : EventArgs
    {
        public SerialMessage Message { get; }

        public AtMessageEventArgs(TransferDirection d, String s)
        {
            Message = new SerialMessage
            {
                Message = s,
                Time = DateTime.Now,
                Direction = d
            };
        }
    }
    public delegate void AtMessageEventHandler(object sender, AtMessageEventArgs e);

    public class AtHelper : BindableBase
    {
        public event AtMessageEventHandler AtMessage;

        private SerialPort serialPort = new SerialPort();
        private BlockingCollection<String> packetBlockingCollection = new BlockingCollection<String>(1);

        private String portName;
        public String PortName
        {
            get { return portName; }
            set { SetProperty(ref portName, value); }
        }

        private String baudRate;
        public String BaudRate
        {
            get { return baudRate; }
            set { SetProperty(ref baudRate, value); }
        }

        public Boolean IsOpen
        {
            get { return serialPort.IsOpen; }
        }

        public void Open()
        {
            serialPort.PortName = PortName;
            serialPort.BaudRate = Convert.ToInt32(BaudRate);
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.Open();
        }

        public void Close()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public void WriteLine(String s)
        {
            // if (serialPort.IsOpen)
            {
                AtMessage(this, new AtMessageEventArgs(TransferDirection.Tx, s));
                while (packetBlockingCollection.TryTake(out _)) { }
                serialPort.WriteLine(s);
            }
        }

        private Boolean ReadResponsePacket(out String packet, int millisecondsTimeout)
        {
            return packetBlockingCollection.TryTake(out packet, millisecondsTimeout);
        }

        public AtHelper(AtMessageEventHandler handler)
        {
            AtMessage += handler;

            Task.Run(() => {
                while (true)
                {
                    try
                    {
                        if (!serialPort.IsOpen) continue;

                        String s = serialPort.ReadLine().Trim(' ', '\r', '\n');
                        if (s != "")
                        {
                            AtMessage(this, new AtMessageEventArgs(TransferDirection.Rx, s));
                            // Log.Information("AT Rx: " + s);
                            if (!packetBlockingCollection.TryAdd(s))
                                Debug.WriteLine("无法在添加包了");
                            else
                                Debug.WriteLine("添加包成功");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
        }

        public Boolean CheckCommand(String tx, String rx, Int32 time)
        {
            String responsePacket = null;
            WriteLine(tx);
            if (!ReadResponsePacket(out responsePacket, time))
                return false;

            return responsePacket == rx;
        }
    }
}
