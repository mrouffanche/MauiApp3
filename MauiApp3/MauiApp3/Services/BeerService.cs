using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using MauiApp3.Models;

namespace MauiApp3.Services
{
    public class BeerService
    {
        private readonly HttpClient _httpClient;

        public BeerService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BeerClient");
        }

        public async Task<List<Beer>> GetBeersAsync()
        {
            var beers = new List<Beer>();

            for (int i = 0; i < 30; i++)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"beers/ale/{i}");
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();

                    if (content.Trim().StartsWith("["))
                    {
                        var batch = JsonConvert.DeserializeObject<List<Beer>>(content);
                        if (batch != null)
                            beers.AddRange(batch);
                    }
                    else
                    {
                        var singleBeer = JsonConvert.DeserializeObject<Beer>(content);
                        if (singleBeer != null)
                            beers.Add(singleBeer);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"API Error at index {i}: {ex}");
                }
            }

            return beers;
        }


        public async Task<Beer> GetBeerAsync(int beerId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"beers/ale/{beerId}");
                response.EnsureSuccessStatusCode();
        
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Beer>(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"API Error: {ex}");
                return null;
            }
        }
    }
}