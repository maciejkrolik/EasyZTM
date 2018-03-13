using EasyZTM.Models;
using System.Collections.Generic;
using System.Linq;
using Prism.Services;
using SQLite;

namespace EasyZTM.Services
{
    public class SqlBusStopService : ISqlBusStopService
    {
        private readonly SQLiteConnection _conn;

        public SqlBusStopService(IDependencyService dependencyService)
        {
            _conn = dependencyService.Get<IFileAccessHelper>().GetConnection();
            _conn.CreateTable<SqlBusStop>();
        }

        public void AddBusStopToFavourites(int stopId)
        {
            var busStop = GetBusStop(stopId);
            busStop.isFavourite = true;
            _conn.Update(busStop);

        }

        public void DeleteBusStopFromFavourite(int stopId)
        {
            var busStop = GetBusStop(stopId);
            busStop.isFavourite = false;
            _conn.Update(busStop);
        }

        public SqlBusStop GetBusStop(int stopId)
        {
            var busStop = _conn.Table<SqlBusStop>()
                               .Where(x => x.StopId == stopId)
                               .SingleOrDefault();
            return busStop;
        }

        public List<SqlBusStop> GetAllStops()
        {
            return _conn.Table<SqlBusStop>().ToList();
        }

        public List<SqlBusStop> GetAllFavouriteStops()
        {
            var busStops = _conn.Table<SqlBusStop>()
                                .Where(x => x.isFavourite == true)
                                .ToList();

            return busStops;
        }
    }
}