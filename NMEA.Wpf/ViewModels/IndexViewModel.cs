using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMEA.Wpf.Common.Navigation;
using NMEA.Wpf.Services;
using Prism.Ioc;

namespace NMEA.Wpf.ViewModels
{
    public class IndexViewModel : GeneralBase
    {
        public IndexViewModel(IContainerProvider provider) : base(provider)
        {
        }
    }
}
