using BudSystem.WorldBank;

namespace BudSystem;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var isoCode = Console.ReadLine();

            var country = WorldBankApi.GetCountry(isoCode!);

            Console.WriteLine(country!.name);
            Console.WriteLine(country.region.value);
            Console.WriteLine(country.capitalCity);
            Console.WriteLine(country.longitude);
            Console.WriteLine(country.latitude);
        }
        catch (Exception ex) when (ex is ArgumentNullException || ex is ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Invalid ISO code.\n{ex.Message}");
        }
            
    }
}