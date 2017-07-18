using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.models
{
    public class Trip
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime dataCreated { get; set; }
        public string username { get; set; }

        public ICollection<Stop> stops { get; set; }
    }
}
