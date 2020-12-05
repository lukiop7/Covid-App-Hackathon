using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatsWorld
{
    public class GetStatsScrapper
    {
        public async Task<List<List<StatsFromCountryModel>>> GetStatics(){
        List <ModelCountry> countries = await new StatsWorldScrapper().GetCountriesNames();
        List<StatsFromCountryModel> oneCountry = new List<StatsFromCountryModel>(); 
        List<List<StatsFromCountryModel>> allCountries = new List<List<StatsFromCountryModel>>();
        foreach (var country in countries)
        {
            String apiForm;
            apiForm = $"https://api.covid19api.com/total/dayone/country/{country.Country}";
            oneCountry = await new CallAPI().GetProductAsync(apiForm);
            allCountries.Add(oneCountry);
        }

        return allCountries;

        }
    }
}