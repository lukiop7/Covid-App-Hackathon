using System.Threading.Tasks;

namespace PunktPobran
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await new PunktPobranScrapper().GetPunktyPobran();
        }
    }
}