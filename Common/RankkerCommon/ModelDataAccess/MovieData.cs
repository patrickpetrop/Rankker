using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RankkerCommon.DataAccess;
using RankkerCommon.Models;

namespace RankkerCommon.ModelDataAccess
{
    public class MovieData : IMovieData
    {
        private readonly IConfiguration _configuration;
        private readonly ISqlDataAccess _sqlDataAccess;

        public MovieData(IConfiguration configuration, ISqlDataAccess sqlDataAccess)
        {
            _configuration = configuration;
            _sqlDataAccess = sqlDataAccess;
        }
        public List<Movie> GetAllMovies(string connectionStringName)
        {
            var connectionString = _configuration.GetConnectionString(connectionStringName);
            var movies = new List<Movie>();
            try
            {
                _sqlDataAccess.StartTransaction(connectionString);

                movies = _sqlDataAccess.LoadDataInTransaction<Movie, dynamic>(StoredProcedures.MOVIE_GETALL, new { });

            }
            catch
            {
                //TODO log error
            }

            return movies;
        }

        public List<Movie> GetAllMoviesAndGenres(string connectionString)
        {
            throw new System.NotImplementedException();
        }
    }
    
}