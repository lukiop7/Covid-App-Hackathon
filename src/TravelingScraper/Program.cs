using System;
using System.Threading.Tasks;

namespace TravelingScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(await new TravelingScraper().GetTravelingInformation());
        }
    }
}
