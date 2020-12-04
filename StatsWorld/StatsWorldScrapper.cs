using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PunktPobran;

namespace StatsWorld
{
    public class StatsWorldScrapper
    {
        public async Task<List<Model>> GetCountries()
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("https://api.covid19api.com/countries")
            };
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //var url = "https://pacjent.gov.pl/sites/default/files/2020-12/Punkty_pobran_0412%20%281%29.json";
            HttpResponseMessage response = await client.GetAsync("https://api.covid19api.com/countries");
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            List<Model> items = JsonConvert.DeserializeObject<List<Model>>(resp);

            return items;
        }
    }
}