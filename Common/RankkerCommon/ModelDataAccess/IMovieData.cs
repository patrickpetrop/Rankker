using System.Collections.Generic;
using RankkerCommon.Models;

namespace RankkerCommon.ModelDataAccess
{
    public interface IMovieData
    {
        List<Movie> GetAllMovies(string connectionString);
        List<Movie> GetAllMoviesAndGenres(string connectionString);
    }
}