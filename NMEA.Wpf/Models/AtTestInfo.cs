using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA.Wpf.Models
{
    public class AtTestInfo
    {
        public String Command { get; set; }
        public String Return { get; set; }
        public Int32 Timeout { get; set; }
        public Int32? Retry { get; set; }
        public Int32? Next { get; set; }

        public AtTestInfo(String cmd, String ret, Int32 time, Int32? retry = null, Int32? next = null)
        {
            Command = cmd;
            Return = ret;
            Timeout = time;
            Retry = retry;
            Next = next;
        }
    }
}
