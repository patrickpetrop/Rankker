using System.Collections.Generic;
using RankkerCommon.Models;

namespace RankkerCommon.ModelDataAccess
{
    public interface IMovieGenreData
    {
        List<MovieGenre> GetAllMovieGenres(string connectionString);
    }
}