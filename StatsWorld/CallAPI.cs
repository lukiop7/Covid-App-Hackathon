using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using System.Threading.Tasks;

namespace StatsWorld
{
    public class CallAPI
    {
        public async Task<List<StatsFromCountryModel>> GetProductAsync(string path)
        {
            HttpClient client = new HttpClient();
            var product = new List<StatsFromCountryModel>();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<List<StatsFromCountryModel>>();
            }
            return product;
            
        }

    }
}