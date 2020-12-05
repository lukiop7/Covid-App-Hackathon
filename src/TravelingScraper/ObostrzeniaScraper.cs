using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingScraper
{
    public class ObostrzeniaScraper
    {
        private readonly string siteUrl = "https://www.gov.pl/web/koronawirus/aktualne-zasady-i-ograniczenia";

        public async Task<string> GetObostrzenia()
        {
            var browser = new ScrapingBrowser();
            WebPage page = await browser.NavigateToPageAsync(new Uri(this.siteUrl));
            IEnumerable<HtmlAgilityPack.HtmlNode> list = page.Html.SelectNodes(".//article").ToList();
            return list.First().InnerHtml;
        }
    }
}
