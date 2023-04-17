using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace NMEA.Wpf.Models
{
    public class MenuBar : BindableBase
    {
        public String Name { get; set; }
        public String Icon { get; set; }
        public String Navigation { get; set; }
    }
}
