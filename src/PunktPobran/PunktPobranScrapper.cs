using Newtonsoft.Json;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static List<GroupedModel> GetPunktyPobran()
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

            var json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory+@"\input.json"));
                List<Model> items = JsonConvert.DeserializeObject<List<Model>>(json);
            List<GroupedModel> grouped = new List<GroupedModel>();
            var names = items.GroupBy(x => x.Województwo).Select(g => g.First()).ToList();
            
                foreach(var name in names)
            {
                var cities = items.Where(x => x.Województwo.Equals(name.Województwo)).ToList();
                var modelToGroup = new GroupedModel();
                List<TownPoints> towns = new List<TownPoints>();
                modelToGroup.Województwo = name.Województwo;

                var citiesUnique = items.Where(x=>x.Województwo.Equals(name.Województwo)).GroupBy(x => x.adr_lok_miejsc).Select(g => g.First()).ToList();
                foreach (var city in citiesUnique)
                {
                    var town = new TownPoints();
                    List<Point> points = new List<Point>();
                    town.City = city.adr_lok_miejsc;
                    var citiesPoints = items.Where(x => x.adr_lok_miejsc.Equals(town.City)).ToList();
                    foreach(var point in citiesPoints)
                    {
                        var townPoint = new Point();
                        townPoint.adr_lok_kod_poczt = point.adr_lok_kod_poczt;
                        townPoint.adr_lok_miejsc = point.adr_lok_miejsc;
                        townPoint.adr_lok_nr_domu = point.adr_lok_nr_domu;
                        townPoint.adr_lok_nr_lokalu = point.adr_lok_nr_lokalu;
                        townPoint.adr_lok_ulica = point.adr_lok_ulica;
                        townPoint.nazwa_swd = point.nazwa_swd;
                        townPoint.telefon_rej = point.telefon_rej;
                        townPoint.harm_pon = point.harm_pon;
                        townPoint.harm_wt = point.harm_wt;
                        townPoint.harm_sr = point.harm_sr;
                        townPoint.harm_czw = point.harm_czw;
                        townPoint.harm_pt = point.harm_pt;
                        townPoint.harm_sob = point.harm_sob;
                        townPoint.harm_niedz = point.harm_niedz;
                        points.Add(townPoint);
                    }
                    town.Points = points;
                    towns.Add(town);
                }
                modelToGroup.TownPointsList = towns.OrderBy(c=>c.City).ToList();
                grouped.Add(modelToGroup);
            }

                return grouped;

            
        }
    }
}
