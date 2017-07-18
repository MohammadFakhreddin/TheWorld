using System;
namespace TheWorld.models
{
    public class Stop
    {
        public int id { get; set; }
        public string name { get; set; }
        public double latitude { get; set; }
        public double longtitude { get; set; }
        public int order { get; set; }
        public DateTime arraival { get; set; }
    }
}