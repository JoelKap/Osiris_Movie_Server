using Osiris_Movie_WebApp.Models;
using System.Threading.Tasks;

namespace Osiris_Movie_WebApp.Service
{
    public interface IMovie
    {
       Task<MovieModel> GetMovie(string name);

        Task<SeriesModel> GetSeriesByMultipleParameters(string name, string season, string episode);
    }
}
