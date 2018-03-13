using System.Collections.Generic;

namespace EasyZTM.Models
{
    public class JsonBusStop
    {
        public string LastUpdate { get; set; }
        public List<Delay> Delay { get; set; }
    }
}