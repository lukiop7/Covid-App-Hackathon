using ScrapySharp.Extensions;
using ScrapySharp.Network;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelingScraper
{
    public class TravelingScraper
    {
        private readonly string siteUrl = "https://www.gov.pl/web/dyplomacja/koronawirus-podroze-za-granice";

        public async Task<TravelingInformation> GetTravelingInformation()
        {
            var browser = new ScrapingBrowser();
            WebPage page = await browser.NavigateToPageAsync(new Uri(this.siteUrl));
            IEnumerable<HtmlAgilityPack.HtmlNode> list = page.Html.CssSelect("Table");

            Dictionary<string, RegionInformation> regions = new Dictionary<string, RegionInformation>();

            foreach (HtmlAgilityPack.HtmlNode element in list)
            {
                var region = new RegionInformation()
                {
                    Name = element.PreviousSibling.PreviousSibling.InnerText,
                };

                foreach (HtmlAgilityPack.HtmlNode l in element.FirstChild.NextSibling.ChildNodes.Where(n => n.Name.ToLower() == "tr"))
                {
                    List<HtmlAgilityPack.HtmlNode> vc = l.ChildNodes.Where(n => n.Name.ToLower() == "td").ToList();
                    var name = vc.FirstOrDefault().InnerText;
                    var vv = vc.ElementAtOrDefault(1)?.InnerText;
                    region.Countries.Add(name, new CountryInformation()
                    {
                        Name = name,
                        v = vv,
                    });
                }

                regions.Add(region.Name, region);
            }

            return new TravelingInformation()
            {
                Regions = regions,
            };

        }
    }

    public class TravelingInformation
    {
        public Dictionary<string, RegionInformation> Regions { get; set; } = new Dictionary<string, RegionInformation>();
    }

    public class RegionInformation
    {
        public string Name { get; set; }

        public Dictionary<string, CountryInformation> Countries { get; set; } = new Dictionary<string, CountryInformation>();
    }

    public class CountryInformation
    {
        public string Name { get; set; }

        public string v { get; set; }
    }
}
