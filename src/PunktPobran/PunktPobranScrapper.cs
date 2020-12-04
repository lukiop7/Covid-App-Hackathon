using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PunktPobran
{
    public class PunktPobranScrapper
    {
        public async Task<List<Model>> GetPunktyPobran()
        {
            using var client = new HttpClient
            {
                BaseAddress = new Uri("https://pacjent.gov.pl/sites/default/files/2020-12/Punkty_pobran_0412%20%281%29.json")
            };
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //var url = "https://pacjent.gov.pl/sites/default/files/2020-12/Punkty_pobran_0412%20%281%29.json";
            HttpResponseMessage response = await client.GetAsync("https://pacjent.gov.pl/sites/default/files/2020-12/Punkty_pobran_0412%20%281%29.json");
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            List<Model> items = JsonConvert.DeserializeObject<List<Model>>(resp);

            items.ForEach(Console.WriteLine);

            return items;

        }
    }
}
