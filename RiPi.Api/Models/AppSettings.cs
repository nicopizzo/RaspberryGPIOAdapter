using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiPi.Api.Models
{
    public class AppSettings
    {
        public string AllowedHosts { get; set; }
        public string PinFileDirectory { get; set; }
    }
}
