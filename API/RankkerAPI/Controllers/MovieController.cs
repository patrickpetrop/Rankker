using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RankkerAPI.Helpers;
using RankkerCommon.AutoMapper;
using RankkerCommon.DTOs;
using RankkerCommon.ModelDataAccess;
using RankkerCommon.Models;

namespace RankkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieGenreData _movieGenreData;
        private readonly string _tmdbApiKey;
        private readonly string _connectionString;

        public MovieController(IConfiguration configuration, ILogger<MovieController> logger, IMovieGenreData movieGenreData)
        {
            _logger = logger;
            _movieGenreData = movieGenreData;
            _tmdbApiKey = GetConfigurationValues.GetTmdbApiKey(configuration);
            _connectionString = GetConfigurationValues.GetConnectionString(configuration);
        }

        //Comment to add in info for MovieGenres
        //Need to add in MovieGenre info when pulling movies
        //Must add it here

        [HttpGet("moviegenre")]
        public async Task<IActionResult> GetAllMovieGenres()
        {
            var movieGenres = _movieGenreData.GetAllMovieGenres(_connectionString);

            return Json(new {movieGenres});
        }




        [HttpGet]
        public async Task<IActionResult> CreateInitalMovie()
        {
            //            var jwtSecretKey = _configuration.GetSection("JwtSecretKey").Value;
            //
            //
            //            var tmdbApiKey = _configuration.GetSection("TMDB_API_Key").Value;
            //
            //
            //            var conString = _configuration.GetConnectionString("DefaultConnection");
            //
            //            return Json(new {jwtSecretKey, tmdbApiKey, conString});


//            _logger.LogInformation("Here is info message from our values controller.");
//            _logger.LogCritical("Here is critical error message from our values controller.");
//            _logger.LogError("Here is error message from our values controller.");

            var movie = new Movie
            {
                Name = "TempName",
                Tagline = "tag",
                Overview = "over",
                TmdbId = 12345,
                ImdbId = "tff",
                TmdbPosterPath = "fdsa",
                TmdbBackdropPath = "wesdfa",
                Status = "eee",
                DateUpdated = DateTime.UtcNow
            };

            var temp = AutoMapperConfiguration.Mapper.Map<MovieDTO>(movie);
            
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                //Remove Dapper from RankkerAPI
                var insertedId = connection.Query<int>("dbo.Movie_Insert",
                    temp, commandType: CommandType.StoredProcedure).Single();
            }
            
            return new OkObjectResult("Movie Created");
        }



    }
}