using LOL.Models;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace LOL
{
    internal class Program
    {
        static string version = "1.0";
        static List<Champion> champions = new List<Champion>();

        static async Task Main(string[] args)
        {
            await VerziokBetoltes();
            Console.WriteLine(version);
            await LoadChampions();
        }

        static async Task LoadChampions()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    string url = $"https://ddragon.leagueoflegends.com/cdn/{version}/data/hu_HU/champion.json";

                    var responseApi = await client.GetStringAsync(url);

                    var response = JsonSerializer.Deserialize<ChampionData>(responseApi);
                    champions = response.Data.Values.ToList();

                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Kapcsolódási hiba: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Átalakítási hiba: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
        }

        static async Task VerziokBetoltes()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    string url = "https://ddragon.leagueoflegends.com/api/versions.json";

                    var responseApi = await client.GetStringAsync(url);

                    string[] response = JsonSerializer.Deserialize<string[]>(responseApi);
                    version = response[0];
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Kapcsolódási hiba: {httpEx.Message}");
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Átalakítási hiba: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba: {ex.Message}");
            }
        }
    }
}
