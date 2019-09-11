using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Osiris_Movie_WebApp.Models;
using Osiris_Movie_WebApp.Service;
using System.Threading.Tasks;

namespace Osiris_Movie_WebApp.Controllers
{
    [Route("api/Movie")]
    public class MovieController : ControllerBase
    {

        private readonly IMovie _movie; 

        public MovieController(IMovie movie)
        {
            _movie = movie;
        }


        // Get: api
        [Route("[action]")]
        [HttpGet("{name}")]
        [Authorize]
        public async Task<MovieModel> GetMovie(string name)
        {
            var movie = await _movie.GetMovie(name);
            return movie;
        }
         
        // Get: api
        [Route("[action]")]
        [HttpGet("{name}/{season}/{episode}")]
        [Authorize]
        public async Task<SeriesModel> GetSeriesByMultipleParameters(string name, string season, string episode)
        {
            return await _movie.GetSeriesByMultipleParameters(name, season, episode);
        }
    }
}
