using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Osiris_Movie_WebApp.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Osiris_Movie_WebApp.Service
{
    public class MovieService : IMovie
    {
        private readonly IOptions<SecurityModel> appSettings;
        private IMemoryCache _cache;
        public MovieService(IOptions<SecurityModel> options, IMemoryCache memoryCache)
        {
            appSettings = options;
            _cache = memoryCache;
        }

        public async Task<MovieModel> GetMovie(string name)
        {
            var movie = CreateEmptyMovie();
            if (!_cache.TryGetValue("MovieDetail", out movie))
            {
                if (movie == null)
                {
                    movie = await GetMovieByName(name);
                }
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(3));
                _cache.Set("MovieDetail", movie, cacheEntryOptions);
            }
            return movie;
        }

        public async Task<SeriesModel> GetSeriesByMultipleParameters(string name, string season, string episode)
        {
            var actualUrl = appSettings.Value.Url + "?t=" + name + "&Season=" + season + "&Episode=" + episode + "&apikey=" + appSettings.Value.APIKey;

            HttpClient client = CreateWebClientAndAddHeaders();
            var response = await client.GetAsync(actualUrl);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SeriesModel>(content);
        }

        private static MovieModel CreateEmptyMovie()
        {
            return new MovieModel(); 
        }

        private async Task<MovieModel> GetMovieByName(string name)
        {
            var actualUrl = appSettings.Value.Url + "?t=" + name + "&apikey=" + appSettings.Value.APIKey;

            HttpClient client = CreateWebClientAndAddHeaders();
            var response = await client.GetAsync(actualUrl);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MovieModel>(content);
        }

        private HttpClient CreateWebClientAndAddHeaders()
        {
            HttpClient client = new HttpClient();
            return client;
        }
    }
}
