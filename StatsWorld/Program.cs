using System;
using System.Threading.Tasks;


namespace StatsWorld
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new StatsWorldScrapper().GetPunktyPobran();
        }
    }
}