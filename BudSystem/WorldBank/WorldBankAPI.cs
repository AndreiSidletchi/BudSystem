using BudSystem.WorldBank.ModelJSON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BudSystem.WorldBank;

public class WorldBankApi
{
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri(@"http://api.worldbank.org/v2/country/")
    };

    public static Country? GetCountry(string isoCode)
    {
        if (string.IsNullOrEmpty(isoCode))
        {
            throw new ArgumentNullException(nameof(isoCode));
        }
        if (!IsCountryCodeValid(isoCode))
        {
            throw new ArgumentOutOfRangeException(nameof(isoCode));
        }

        var response = Client.GetAsync(isoCode + "?format=json").Result;

        var body = response.Content.ReadAsStringAsync().Result;
        // json format: [{header},[array of countries]]
        var json = (JArray)JsonConvert.DeserializeObject(body);
        var header = JsonConvert.DeserializeObject<Header>(json.First.ToString());
        var countries = JsonConvert.DeserializeObject<List<Country>>(json.Last.ToString());

        return countries.FirstOrDefault();
    }

    private static bool IsCountryCodeValid(string countryCode)
    {
        var isoCodes = ISO._3166.CountryCodesResolver.GetList();
        return isoCodes.Any(x => x.Alpha3 == countryCode.ToUpper() || x.Alpha2 == countryCode.ToUpper());
    }
}