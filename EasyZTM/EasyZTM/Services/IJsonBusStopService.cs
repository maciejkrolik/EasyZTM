using System.Collections.Generic;
using System.Threading.Tasks;
using EasyZTM.Models;

namespace EasyZTM.Services
{
    public interface IJsonBusStopService
    {
        Task<List<Delay>> GetAllBusesAsync(int stopNumber);
        Task<Delay> GetSpecificBusAsync(int positionInList, int stopNumber);
    }
}