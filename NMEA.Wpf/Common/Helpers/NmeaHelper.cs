using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NmeaParser;
using Prism.Mvvm;

namespace NMEA.Wpf.Common.Helpers
{
    public class NmeaHelper : BindableBase
    {
        private SerialPort serialPort = new SerialPort();
        private NmeaDevice device = null;

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

        public async Task OpenAsync()
        {
            serialPort.PortName = PortName;
            serialPort.BaudRate = Convert.ToInt32(BaudRate);
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.Open();
            await device.OpenAsync();
        }

        public async Task CloseAsync()
        {
            if (device.IsOpen)
            {
                serialPort.Close();
                await device.CloseAsync();
            }
        }

        public NmeaHelper(EventHandler<NmeaMessageReceivedEventArgs> handler)
        {
            device = new SerialPortDevice(serialPort);
            device.MessageReceived += handler;
        }
    }
}
