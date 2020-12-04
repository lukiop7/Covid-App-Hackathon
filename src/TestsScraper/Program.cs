using System.Threading.Tasks;

namespace TestsScraper
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var result = await new TestsScrapper().GetPerformedTestsWeekly();
        }
    }
}
