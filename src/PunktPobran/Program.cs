using System.Threading.Tasks;

namespace PunktPobran
{
    internal class Program
    {
        private static void  Main(string[] args)
        {
            var list = PunktPobranScrapper.GetPunktyPobran();
        }
    }
}