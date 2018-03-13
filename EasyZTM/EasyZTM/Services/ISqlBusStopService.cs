using System.Collections.Generic;
using EasyZTM.Models;

namespace EasyZTM.Services
{
    public interface ISqlBusStopService
    {
        void AddBusStopToFavourites(int stopId);
        void DeleteBusStopFromFavourite(int stopId);
        List<SqlBusStop> GetAllFavouriteStops();
        List<SqlBusStop> GetAllStops();
        SqlBusStop GetBusStop(int stopId);
    }
}