using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatsWorld
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var result = await new GetStatsScrapper().GetStatics();
        }
    }
}