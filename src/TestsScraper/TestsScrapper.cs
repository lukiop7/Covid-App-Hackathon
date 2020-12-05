using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using ScrapySharp.Extensions;
using ScrapySharp.Network;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestsScraper
{
    public class Tests
    {
        public DateTime date;
        public Dictionary<string, int> city = new Dictionary<string, int>();

        public Tests(DateTime date, Dictionary<string, int> dictionary)
        {
            this.date = date;
            city = dictionary;
        }
    }

    public class TestsScrapper
    {
        private const string siteUrl = "https://www.gov.pl/web/zdrowie/liczba-wykonanych-testow";

        public async Task<Dictionary<DateTime, Dictionary<string, int>>> GetPerformedTestsWeekly()
        {

            var result = new Dictionary<DateTime, Dictionary<string, int>>();

            var browser = new ScrapingBrowser();
            WebPage page = await browser.NavigateToPageAsync(new Uri(siteUrl));
            HtmlAgilityPack.HtmlNode link = page.Html.SelectNodes(".//a").Where(n => n.HasClass("file-download")).First();
            string fileUrl = $"https://www.gov.pl{link.GetAttributeValue("href")}";

            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(fileUrl);
            IWorkbook wb = new XSSFWorkbook(await response.Content.ReadAsStreamAsync());
            int sheets = wb.NumberOfSheets;

            for (int i = 0; i < sheets; i++)
            {
                Dictionary<string, int> weekData;
                DateTime date;
                ProcessWeek(wb, i, out weekData, out date);

                result.Add(date, weekData);
            }

            return result;
        }
        public async Task<List<Tests>> GetPerformedTestsWeekly2()
        {

            //var result = new Dictionary<DateTime, Dictionary<string, int>>();
            var result = new List<Tests>();

            var browser = new ScrapingBrowser();
            WebPage page = await browser.NavigateToPageAsync(new Uri(siteUrl));
            HtmlAgilityPack.HtmlNode link = page.Html.SelectNodes(".//a").Where(n => n.HasClass("file-download")).First();
            string fileUrl = $"https://www.gov.pl{link.GetAttributeValue("href")}";

            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(fileUrl);
            IWorkbook wb = new XSSFWorkbook(await response.Content.ReadAsStreamAsync());
            int sheets = wb.NumberOfSheets;

            for (int i = 0; i < sheets; i++)
            {
                Dictionary<string, int> weekData;
                DateTime date;
                ProcessWeek(wb, i, out weekData, out date);

                result.Add(new Tests(date, weekData));
            }

            return result;
        }

        private static void ProcessWeek(IWorkbook wb, int i, out Dictionary<string, int> weekData, out DateTime date)
        {
            weekData = new Dictionary<string, int>();
            string dateStr = wb.GetSheetName(i).Replace("TygodnioweDane", "");
            date = new DateTime(2020, int.Parse(dateStr.Substring(3, 2)), int.Parse(dateStr.Substring(0, 2)));
            wb.SetActiveSheet(i);
            Console.WriteLine(date);

            for (int r = 0; r < 16; r++)
            {
                var row = wb.GetSheetAt(i).GetRow(r + 2);
                var title = row.GetCell(1).StringCellValue;
                var value = row.GetCell(2).NumericCellValue;
                weekData.Add(title, (int)value);
            }
        }
    }

}
