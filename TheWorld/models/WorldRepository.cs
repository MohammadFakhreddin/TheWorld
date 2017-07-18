using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.models
{
    public class WorldRepository : IWorldRepository//The idea of this is to making testing easier
                                                   //We need interface for mocking up
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext _context,ILogger<WorldRepository> _logger)
        {
            this._context = _context;
            this._logger = _logger;
        }

        public void addTrip(Trip trip)
        {
            _context.trips.Add(trip);
        }

        public void addStop(string tripName,Stop stop,string username)
        {
            Trip trip = getTripByName(tripName,username);
            if (trip != null)
            {
                trip.stops.Add(stop);
                _context.stops.Add(stop);
            }
        }

        public IEnumerable<Trip> getAllTrips()
        {
            return _context.trips.ToList();
        }

        public Trip getTripByName(string tripName,string username)
        {
            return _context.trips
                .Where(t => t.name == tripName && t.username==username)
                .Include(t=>t.stops)
                .FirstOrDefault();
        }

        public async Task<bool> saveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public IEnumerable<Trip> getAllTripsWithStops(string name)
        {
            try {
                return _context.trips
                    .Include(t => t.stops)
                    .OrderBy(t => t.name)
                    .Where(t => t.username == name)
                    .ToList();
            }catch(Exception e)
            {
                _logger.LogError(e.Message + "\n" + e.StackTrace);
            }
            return null;
        }
    }
}
