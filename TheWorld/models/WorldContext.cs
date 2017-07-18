using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace TheWorld.models
{
    public class WorldContext:IdentityDbContext<WorldUser>
    {
        private IConfigurationRoot _config;
        public WorldContext(IConfigurationRoot _config,DbContextOptions options)
            :base(options)
        {
            this._config = _config;
        }

        public DbSet<Trip> trips { get; set; }
        public DbSet<Stop> stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);
            optionBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }
    }
}
