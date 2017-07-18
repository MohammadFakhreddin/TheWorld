using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> getAllTrips();

        void addTrip(Trip trip);

        Task<bool> saveChangesAsync();
        Trip getTripByName(string tripName,string username);
        void addStop(string tripName, Stop newStop,string username);
        IEnumerable<Trip> getAllTripsWithStops(string name);
    }
}