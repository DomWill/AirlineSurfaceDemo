using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AirlineSurfaceDemo
{
    [DataContract(Name = "airportResult")]
    class AirportInfoResult
    {
        [DataMember(Name = "AirportInfoResult")]
        public Airport AirportResult { get; set; }
    }
}
