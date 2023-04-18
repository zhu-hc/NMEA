using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NmeaParser.Messages;
using NmeaParser.Messages.Garmin;
using Prism.Mvvm;

namespace NMEA.Wpf.Models
{
    public class NmeaMessage : BindableBase
    {
        private Rmc rmc;
        public Rmc Rmc
        {
            get { return rmc; }
            set { SetProperty(ref rmc, value); }
        }

        private Gga gga;
        public Gga Gga
        {
            get { return gga; }
            set { SetProperty(ref gga, value); }
        }

        private Gsa gsa;
        public Gsa Gsa
        {
            get { return gsa; }
            set { SetProperty(ref gsa, value); }
        }

        private Gll gll;
        public Gll Gll
        {
            get { return gll; }
            set { SetProperty(ref gll, value); }
        }

        private Pgrme pgrme;
        public Pgrme Pgrme
        {
            get { return pgrme; }
            set { SetProperty(ref pgrme, value); }
        }

        private Vtg vtg;
        public Vtg Vtg
        {
            get { return vtg; }
            set { SetProperty(ref vtg, value); }
        }
    }
}
