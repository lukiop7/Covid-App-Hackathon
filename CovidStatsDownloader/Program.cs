using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using CsvHelper.Configuration.Attributes;
using CsvHelper;
using System.Linq;

namespace CovidStatsDownloader
{
    public static class Program
    {
        public static async System.Threading.Tasks.Task<List<List<PolandStats>>> GetPolandStatsAsync()
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            var page = await browser.NavigateToPageAsync(new Uri(@"https://www.gov.pl/web/koronawirus/pliki-archiwalne-powiaty?fbclid=IwAR03yH7iHK_dpdwbT62ftEcH1uIJtwbjjoMJiq7hljPyVuqvskawvu_tRMw"));
            var list = page.Html.CssSelect("a");
            List<string> fileUrl = new List<string>();
            foreach (var link in list)
            {
                if (link.OuterHtml.Contains("file-download"))
                {
                    string url = "https://www.gov.pl" + link.Attributes["href"].Value;
                    fileUrl.Add(url);
                }
            }
            List<List<PolandStats>> statsAllFiles = new List<List<PolandStats>>();
            foreach (var url in fileUrl)
            {
                statsAllFiles.Add(await GetCSVAsync(url));
            }
            return statsAllFiles;
        }

        public static async System.Threading.Tasks.Task<List<PolandStats>> GetCSVAsync(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)await req.GetResponseAsync();

            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1250));

            List<PolandStats> records;

            using (CsvReader csv = new CsvReader(sr,System.Globalization.CultureInfo.CurrentCulture))
            {
                csv.Configuration.Delimiter = ";";
                records = csv.GetRecords<PolandStats>().ToList();
            }
            sr.Close();

            return records;
        }

    }
    public class PolandStats
    {
        [Index(0)]
        public string Province { get; set; }
        [Index(1)]
        public string City { get; set; }
        [Index(2)]
        public int? TotalCases { get; set; }
        [Index(3)]
        public double? Cases10k { get; set; }
        [Index(4)]
        public int? Deaths { get; set; }
        [Index(5)]
        public int? DeathsCOVID { get; set; }
        [Index(6)]
        public int? DeathsDiseases { get; set; }
        [Index(7)]
        public string TerritoryCode { get; set; }
    }
}
