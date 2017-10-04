using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace AirlineSurfaceDemo
{
    [DataContract(Name = "airport")]
    class Airport
    {
        [DataMember(Name = "name")]
        public string Name;
        [DataMember(Name = "airport_code")]
        public string Code;
    }
}
