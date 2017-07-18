using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.models
{
    public class WorldContextSeedData
    {
        private WorldContext _context;
        private UserManager<WorldUser> _userManager;
        public WorldContextSeedData(WorldContext _context,UserManager<WorldUser>_userManager)
        {
            this._context = _context;
            this._userManager = _userManager;
        }

        public async Task ensureSeedData()
        {
            WorldUser user = await _userManager.FindByEmailAsync("mohammad.fakhreddin@gmail.com");
            if (user==null)
            {
                user = new WorldUser()
                {
                    UserName = "mohammad",
                    Email = "mohammad.fakhreddin@gmail.com",
                };
                var result = await _userManager.CreateAsync(user, "Mamad1234_");
                if(!result.Succeeded)
                {
                    throw new Exception(result.Errors.ToString());
                }      
            }
            if (!_context.trips.Any())
            {
                Trip usTrip = new Trip()
                {
                    dataCreated = DateTime.UtcNow,
                    name = "mamad US Trip",
                    username = "mohammad",//TODO need username
                    stops = new List<Stop>()
                    {
                        new Stop() {  name = "Atlanta, GA", arraival = new DateTime(2014, 6, 4), latitude = 33.748995, longtitude = -84.387982, order = 0 },
                        new Stop() {  name = "New York, NY", arraival = new DateTime(2014, 6, 9), latitude = 40.712784, longtitude = -74.005941, order = 1 },
                        new Stop() {  name = "Boston, MA", arraival = new DateTime(2014, 7, 1), latitude = 42.360082, longtitude = -71.058880, order = 2 },
                        new Stop() {  name = "Chicago, IL", arraival = new DateTime(2014, 7, 10), latitude = 41.878114, longtitude = -87.629798, order = 3 },
                        new Stop() {  name = "Seattle, WA", arraival = new DateTime(2014, 8, 13), latitude = 47.606209, longtitude = -122.332071, order = 4 },
                        new Stop() {  name = "Atlanta, GA", arraival = new DateTime(2014, 8, 23), latitude = 33.748995, longtitude = -84.387982, order = 5 },

                    }
                };
                _context.trips.Add(usTrip);
                _context.stops.AddRange(usTrip.stops);

                Trip worldTrip = new Trip()
                {
                    dataCreated = DateTime.UtcNow,
                    name = "mamadWorldTrip",
                    username = "mohammad",
                    stops = new List<Stop>()
                    {
                        new Stop() { order = 0, latitude =  33.748995, longtitude =  -84.387982, name = "Atlanta, Georgia", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 1, latitude =  48.856614, longtitude =  2.352222, name = "Paris, France", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 2, latitude =  50.850000, longtitude =  4.350000, name = "Brussels, Belgium", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 3, latitude =  51.209348, longtitude =  3.224700, name = "Bruges, Belgium", arraival =new DateTime(2014, 6, 4) },
                        new Stop() { order = 4, latitude =  48.856614, longtitude =  2.352222, name = "Paris, France", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 5, latitude =  51.508515, longtitude =  -0.125487, name = "London, UK", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 6, latitude =  51.454513, longtitude =  -2.587910, name = "Bristol, UK", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 7, latitude =  52.078000, longtitude =  -2.783000, name = "Stretton Sugwas, UK", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 8, latitude =  51.864211, longtitude =  -2.238034, name = "Gloucestershire, UK", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 9, latitude =  52.954783, longtitude =  -1.158109, name = "Nottingham, UK", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 10, latitude =  51.508515, longtitude =  -0.125487, name = "London, UK", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 11, latitude =  55.953252, longtitude =  -3.188267, name = "Edinburgh, UK", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 12, latitude =  55.864237, longtitude =  -4.251806, name = "Glasgow, UK", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 13, latitude =  57.149717, longtitude =  -2.094278, name = "Aberdeen, UK", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 14, latitude =  55.953252, longtitude =  -3.188267, name = "Edinburgh, UK", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 15, latitude =  51.508515, longtitude =  -0.125487, name = "London, UK", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 16, latitude =  52.370216, longtitude =  4.895168, name = "Amsterdam, Netherlands", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 17, latitude =  48.583148, longtitude =  7.747882, name = "Strasbourg, France", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 18, latitude =  46.519962, longtitude =  6.633597, name = "Lausanne, Switzerland", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 19, latitude =  46.021073, longtitude =  7.747937, name = "Zermatt, Switzerland", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 20, latitude =  46.519962, longtitude =  6.633597, name = "Lausanne, Switzerland", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 21, latitude =  53.349805, longtitude =  -6.260310, name = "Dublin, Ireland", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 22, latitude =  54.597285, longtitude =  -5.930120, name = "Belfast, Northern Ireland", arraival =new DateTime(2014, 6, 4)},
                        new Stop() { order = 23, latitude =  53.349805, longtitude =  -6.260310, name = "Dublin, Ireland", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 24, latitude =  47.368650, longtitude =  8.539183, name = "Zurich, Switzerland", arraival =new DateTime(2014, 6, 4) },
                        new Stop() { order = 25, latitude =  48.135125, longtitude =  11.581981, name = "Munich, Germany", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 26, latitude =  50.075538, longtitude =  14.437800, name = "Prague, Czech Republic", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 27, latitude =  51.050409, longtitude =  13.737262, name = "Dresden, Germany", arraival =new DateTime(2014, 6, 4)},
                        new Stop() { order = 28, latitude =  50.075538, longtitude =  14.437800, name = "Prague, Czech Republic", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 29, latitude =  42.650661, longtitude =  18.094424, name = "Dubrovnik, Croatia", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 30, latitude =  42.697708, longtitude =  23.321868, name = "Sofia, Bulgaria", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 31, latitude =  45.658928, longtitude =  25.539608, name = "Brosov, Romania", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 32, latitude =  41.005270, longtitude =  28.976960, name = "Istanbul, Turkey", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 33, latitude =  45.815011, longtitude =  15.981919, name = "Zagreb, Croatia", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 34, latitude =  41.005270, longtitude =  28.976960, name = "Istanbul, Turkey", arraival =new DateTime(2014, 6, 4)},
                        new Stop() { order = 35, latitude =  50.850000, longtitude =  4.350000, name = "Brussels, Belgium", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 36, latitude =  50.937531, longtitude =  6.960279, name = "Cologne, Germany", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 37, latitude =  48.208174, longtitude =  16.373819, name = "Vienna, Austria", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 38, latitude =  47.497912, longtitude =  19.040235, name = "Budapest, Hungary", arraival =new DateTime(2014, 6, 4) },
                        new Stop() { order = 39, latitude =  37.983716, longtitude =  23.729310, name = "Athens, Greece", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 40, latitude =  -25.746111, longtitude =  28.188056, name = "Pretoria, South Africa", arraival =new DateTime(2014, 6, 4) },
                        new Stop() { order = 41, latitude =  43.771033, longtitude =  11.248001, name = "Florence, Italy", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 42, latitude =  45.440847, longtitude =  12.315515, name = "Venice, Italy", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 43, latitude =  43.771033, longtitude =  11.248001, name = "Florence, Italy", arraival =new DateTime(2014, 6, 4)},
                        new Stop() { order = 44, latitude =  41.872389, longtitude =  12.480180, name = "Rome, Italy", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 45, latitude =  28.632244, longtitude =  77.220724, name = "New Delhi, India", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 46, latitude =  27.700000, longtitude =  85.333333, name = "Kathmandu, Nepal", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 47, latitude =  28.632244, longtitude =  77.220724, name = "New Delhi, India", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 48, latitude =  22.1667, longtitude =  113.5500, name = "Macau", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 49, latitude =  22.396428, longtitude =  114.109497, name = "Hong Kong", arraival =new DateTime(2014, 6, 4) },
                        new Stop() { order = 50, latitude =  39.904030, longtitude =  116.407526, name = "Beijing, China", arraival = new DateTime(2014, 6, 4)},
                        new Stop() { order = 51, latitude =  22.396428, longtitude =  114.109497, name = "Hong Kong", arraival =new DateTime(2014, 6, 4)},
                        new Stop() { order = 52, latitude =  1.352083, longtitude =  103.819836, name = "Singapore", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 53, latitude =  3.139003, longtitude =  101.686855, name = "Kuala Lumpor, Malaysia", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 54, latitude =  13.727896, longtitude =  100.524123, name = "Bangkok, Thailand", arraival = new DateTime(2014, 6, 4) },
                        new Stop() { order = 55, latitude =  33.748995, longtitude =  -84.387982, name = "Atlanta, Georgia", arraival =new DateTime(2014, 6, 4) },

                    }
                };

                _context.trips.Add(worldTrip);
                _context.stops.AddRange(worldTrip.stops);
                await _context.SaveChangesAsync();
            }
        }
    }
}
