using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

using ScrapySharp.Extensions;
using ScrapySharp.Network;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Szpitale
{
    public class City
    {
        public string name;
        //public Dictionary<string, string> hospitals;
        public List<string> hospitalName, hospitalAddres;

        public City()
        {
            this.hospitalAddres = new List<string>();
            this.hospitalName = new List<string>();
            //hospitals = new Dictionary<string, string>();
        }
    }

    internal class Program
    {
        private static readonly string siteUrl = @"https://www.gov.pl/web/koronawirus/lista-szpitali?fbclid=IwAR13FDoDSxj8T2zrejrzNN4hC5rbEUZ9ynG9BBla2c_qmt3d4-V3FF5ilwk";

        private static async Task Main(string[] args)
        {
            await ScrapMethod2();
            Console.ReadKey();
        }

        public static async Task ScrapeWebsite()
        {
            //Console.WriteLine("yo");
            var cancellationToken = new CancellationTokenSource();
            var httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(siteUrl);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            //Console.WriteLine("yo");

            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);

            Console.WriteLine(response);
            Console.WriteLine("yo" + document.Url + "  " + document.NodeName);

            //GetScrapeResults(document);
        }
        private static void GetScrapeResults(IHtmlDocument document)
        {
            IEnumerable<IElement> articleLink = null;

            //foreach (var term in QueryTerms)
            //{
            //    articleLink = document.All.Where(x => x.ClassName.Contains("cities"));
            //}

            IEnumerable<IElement> pricesListItemsLinq = document.All
            .Where(m => m.LocalName == "span" && m.ClassList.Equals("c-price"));
            Console.WriteLine(pricesListItemsLinq.Count());

            IElement ultag = document.QuerySelector("ul.law-court__cities");
            //var ultag_html = ultag.ToHtml();

            Console.WriteLine(articleLink.FirstOrDefault() + " 1");
            Console.WriteLine(ultag);
            //Console.WriteLine(ultag_html+ " 1");
            //Console.WriteLine(articleLink.FirstOrDefault() + " 1");
        }

        public static async Task<string> ScrapMethod()
        {
            var browser = new ScrapingBrowser();

            WebPage page = await browser.NavigateToPageAsync(new Uri(siteUrl));

            IEnumerable<HtmlAgilityPack.HtmlNode> list = page.Html.CssSelect("div");

            //Console.WriteLine(list.Count()+" m:");

            IEnumerable<HtmlAgilityPack.HtmlNode> list2 = list.Where(s => s.OuterHtml.Contains("cities") && s.InnerText.Contains("Województwo"));
            //foreach (var l in list2) Console.WriteLine(l.PreviousSibling.InnerText);

            //Console.WriteLine(list2.Last().InnerText);

            return list2.Last().InnerText;
        }


        public static async Task<List<City>> ScrapMethod2()
        {
            var browser = new ScrapingBrowser();

            WebPage page = await browser.NavigateToPageAsync(new Uri(siteUrl));

            IEnumerable<HtmlAgilityPack.HtmlNode> list = page.Html.CssSelect("li");

            Console.WriteLine(list.Count() + " m:");

            IEnumerable<HtmlAgilityPack.HtmlNode> list2 = list.Where(s => s.InnerText.Contains("Województwo"));
            Console.WriteLine(list2.Count() + " m:");
            //foreach (var l in list2) Console.WriteLine(l.InnerText);

            //Console.WriteLine(list2.First().InnerText);
            //Console.WriteLine("\n\n");

            //Console.WriteLine(list2.First().InnerText.Split('\n').First());
            //var tmp = list2.First().InnerText.Split('\n');
            //foreach (var t in tmp) Console.WriteLine(t+"  yoyo  \n\n\n");
            var cities = new List<City>();

            foreach (HtmlAgilityPack.HtmlNode l in list2)
            {
                string text = l + "";
                var tmp = new City();


                string[] s = l.InnerText.Split('\n');
                tmp.name = s[1];

                for (int i = 3; i < s.Count(); i++)
                {
                    if (s[i].Length > 2)
                    {
                        string scity = s[i].Remove(s[i].IndexOf(","));
                        string saddress = s[i].Replace(scity + ",", "");
                        //tmp.hospitals.Add(scity, saddress);
                        tmp.hospitalName.Add(scity);
                        tmp.hospitalAddres.Add(saddress);
                    }
                }

                Console.WriteLine("1: " + tmp.name);
                Console.WriteLine("2: " + tmp.hospitalName.Count);
                Console.WriteLine("3: " + tmp.hospitalAddres.First());
                cities.Add(tmp);
            }

            return cities;
        }
    }
}
