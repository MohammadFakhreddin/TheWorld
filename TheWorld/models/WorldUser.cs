using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.models
{
    public class WorldUser:IdentityUser
    {
        public DateTime firstTrip { get; set; }
    }
}
