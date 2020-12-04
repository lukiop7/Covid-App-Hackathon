using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PunktPobran
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new PunktPobranScrapper().GetPunktyPobran();
        }
    }
}