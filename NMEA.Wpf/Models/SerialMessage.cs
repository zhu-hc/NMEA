using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace NMEA.Wpf.Models
{
    public enum TransferDirection { Tx, Rx }
    public class SerialMessage : BindableBase
    {
        private String message;
        public String Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        private DateTime time;
        public DateTime Time
        {
            get { return time; }
            set { SetProperty(ref time, value); }
        }

        private TransferDirection direction;
        public TransferDirection Direction
        {
            get { return direction; }
            set { SetProperty(ref direction, value); }
        }
    }
}
