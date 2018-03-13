using EasyZTM.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyZTM.Services
{
    public class JsonBusStopService : IJsonBusStopService
    {
        public async Task<List<Delay>> GetAllBusesAsync(int stopNumber)
        {
            string urlContent = await GetJsonStreamAsync(stopNumber);
            JsonBusStop jsonBusStop = JsonConvert.DeserializeObject<JsonBusStop>(urlContent);
            return jsonBusStop.Delay;
        }

        public async Task<Delay> GetSpecificBusAsync(int positionInList, int stopNumber)
        {
            var busStop = await GetAllBusesAsync(stopNumber);
            return busStop[positionInList];
        }

        private async Task<string> GetJsonStreamAsync(int stopNumber)
        {
            HttpClient client = new HttpClient();
            string urlContent = await client.GetStringAsync(Keys.BusStopUrl + stopNumber);
            return urlContent;
        }
    }
}
