using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.viewModels
{
    public class StopViewModel
    {
        [Required]
        [StringLength(100,MinimumLength =5)]
        public string name { get; set; }
        public double latitude { get; set; }
        public double longtitude { get; set; }
        [Required]
        public int order { get; set; }
        [Required]
        public DateTime arraival { get; set; }
    }
}
