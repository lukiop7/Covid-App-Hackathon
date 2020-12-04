using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TravelingScraper
{
    public class TravelingScraper
    {
        private string siteUrl = "https://www.gov.pl/web/dyplomacja/koronawirus-podroze-za-granice";

        public async Task<string> GetTravelingInformation()
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            var page = await browser.NavigateToPageAsync(new Uri(siteUrl));
            var list = page.Html.CssSelect("Table");

            foreach(var element in list)
            {
                var tittle = element.PreviousSibling.PreviousSibling.InnerText;
                Console.WriteLine(tittle);
            }

            return "";

        }
    }

    public class TravelingInformation
    {

    }
}
