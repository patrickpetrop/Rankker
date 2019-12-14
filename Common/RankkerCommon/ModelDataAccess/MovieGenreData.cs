using System.Collections.Generic;
using RankkerCommon.DataAccess;
using RankkerCommon.Models;

namespace RankkerCommon.ModelDataAccess
{
    public class MovieGenreData : IMovieGenreData
    {
        private readonly ISqlDataAccess _sqlDataAccess;

        public MovieGenreData(ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }

        public List<MovieGenre> GetAllMovieGenres(string connectionString)
        {
            var genres = new List<MovieGenre>();
            try
            {
                _sqlDataAccess.StartTransaction(connectionString);

                genres = _sqlDataAccess.LoadDataInTransaction<MovieGenre, dynamic>(StoredProcedures.MOVIEGENRE_GETALL,
                    new { });
            }
            catch
            {
                //TODO log error
            }

            return genres;
        }
    }
}