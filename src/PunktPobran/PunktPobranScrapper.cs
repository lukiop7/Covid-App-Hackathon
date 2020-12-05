using Newtonsoft.Json;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PunktPobran
{
    public class PunktPobranScrapper
    {
        public static async Task<List<Model>> GetPunktyPobran()
        {
            //    var handler = new HttpClientHandler();

            //    handler.ServerCertificateCustomValidationCallback +=
            //                    (sender, certificate, chain, errors) =>
            //                    {
            //                        return true;
            //                    };

            //    using var client = new HttpClient(handler);
            ////    client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            //    client.DefaultRequestHeaders.Accept.Add(
            //        new MediaTypeWithQualityHeaderValue("application/json"));


            //var url = "https://pacjent.gov.pl/sites/default/files/2020-12/Punkty_pobran_0412%20%281%29.json";
            //HttpResponseMessage response = await client.GetAsync("http://pacjent.gov.pl/sites/default/files/2020-12/Punkty_pobran_0412%20%281%29.json");
            //response.EnsureSuccessStatusCode();
            //var resp = await response.Content.ReadAsStringAsync();

            var json = File.ReadAllText(@"../../../Data/input.json");
                List<Model> items = JsonConvert.DeserializeObject<List<Model>>(json);

                items.ForEach(Console.WriteLine);

                return items;

            
        }
    }
}
