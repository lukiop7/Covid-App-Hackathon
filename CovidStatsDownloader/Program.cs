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
using System.Globalization;

namespace CovidStatsDownloader
{
    public static class Program
    {

        public static async System.Threading.Tasks.Task<List<PolandStats>> GetPolandStatsAsync()
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            var page = await browser.NavigateToPageAsync(new Uri(@"https://www.gov.pl/web/koronawirus/pliki-archiwalne-powiaty?fbclid=IwAR03yH7iHK_dpdwbT62ftEcH1uIJtwbjjoMJiq7hljPyVuqvskawvu_tRMw"));
            var list = page.Html.CssSelect("a");
            List<string> fileUrl = new List<string>();
            List<PolandStats> statsAllFiles = new List<PolandStats>();
            foreach (var link in list)
            {
                if (link.OuterHtml.Contains("file-download"))
                {
                    string url = "https://www.gov.pl" + link.Attributes["href"].Value;
                    string date = link.Attributes["aria-label"].Value.Substring(14, 8);
                    date = date.Replace('_', '/').Insert(6,"20");
                    statsAllFiles.Add(new PolandStats(date));
                    fileUrl.Add(url);
                }
            }

            for(int i =0; i < fileUrl.Count; i++)
            {
                statsAllFiles.ElementAt(i).FileStatsList=await GetCSVAsync(fileUrl.ElementAt(i));
            }
            return statsAllFiles;
        }

        public static async System.Threading.Tasks.Task<List<FileStats>> GetCSVAsync(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)await req.GetResponseAsync();

            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1250));

            List<FileStats> records;

            using (CsvReader csv = new CsvReader(sr,System.Globalization.CultureInfo.CurrentCulture))
            {
                csv.Configuration.Delimiter = ";";
                records = csv.GetRecords<FileStats>().ToList();
            }
            sr.Close();

            return records;
        }

    }
    public class FileStats
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

    public class PolandStats
    {
        public List<FileStats> FileStatsList { get; set; }
        public DateTime FileDate { get; set; }

        public PolandStats(string date)
        {
            FileDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.CurrentCulture);
            FileStatsList = new List<FileStats>();
        }
    }

}
