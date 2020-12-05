using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatsWorld
{
    public class GetStatsScrapper
    {
        public async Task<List<List<StatsFromCountryModel>>> GetStatics(){
        List <ModelCountry> countries = await new StatsWorldScrapper().GetCountriesNames();
        //List<StatsFromCountryModel> oneCountry = new List<StatsFromCountryModel>(); 
        List<List<StatsFromCountryModel>> allCountries = new List<List<StatsFromCountryModel>>();
        var r = countries.Select(o =>
        {
            String apiForm = $"https://api.covid19api.com/total/dayone/country/{o.Country}";
            return new CallAPI().GetProductAsync(apiForm);

        }).ToList();
        foreach (var country in r)
        {
            try
            {
                var oneCountry = await country;
                Console.WriteLine("hello");
                allCountries.Add(oneCountry);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
          
        }

        return allCountries;

        }
    }
}