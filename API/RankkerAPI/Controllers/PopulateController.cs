using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Rankker.Common.Tmdb.Imdb;
using RankkerAPI.Helpers;
using RankkerCommon.AutoMapper;
using RankkerCommon.DataAccess;
using RankkerCommon.DTOs;
using RankkerCommon.ModelDataAccess;
using RankkerCommon.Models;
using RankkerCommon.TMDB;

namespace RankkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulateController : Controller
    {

        private readonly ILogger<PopulateController> _logger;
        private readonly IMovieGenreData _movieGenreData;
        private readonly string _tmdbApiKey;
        private readonly string _connectionString;

        public PopulateController(IConfiguration configuration, ILogger<PopulateController> logger, IMovieGenreData movieGenreData)
        {
            _logger = logger;
            _movieGenreData = movieGenreData;
            _tmdbApiKey = GetConfigurationValues.GetTmdbApiKey(configuration);
            _connectionString = GetConfigurationValues.GetConnectionString(configuration);
        }

        [HttpPut("movie/{tmdbMovieId}")]
        public async Task<IActionResult> PopulateMovie(int tmdbMovieId)
        {
            var movieGenres = _movieGenreData.GetAllMovieGenres(_connectionString);

            var movie = await TmdbMovieService.GetMovieByTmdbId(tmdbMovieId, _tmdbApiKey);

            var movieDto = AutoMapperConfiguration.Mapper.Map<MovieDTO>(movie);

            return Json(new {movie});

        }

        [HttpPut("imdbList/{imdbListId}")]
        public async Task<IActionResult> PopulateImdbList(string imdbListId)
        {
            var imdbMovieList = new ImdbMovieList(imdbListId);
            await imdbMovieList.GetListOfMovies();

            return Json(new {imdbMovieList.MovieList, imdbMovieList.ListSize});
        }

        [HttpGet("moviegenre")]
        public async Task<IActionResult> PopulateMovieGenres()
        {
            var result = await TmdbMovieService.GetListOfMovieGenres(_tmdbApiKey);

            foreach (var movieGenre in result)
            {
                try
                {
                    //Remove Dapper from RankkerAPI
                    await _movieGenreData.InsertMovieGenre(_connectionString, movieGenre);
                }
                catch (Exception e)
                {
                    _logger.LogError("Error while inserting movie genre with code " + e.Message);
                }
            }

            return new ContentResult
            {
                Content = new JObject() { { "message", result.Count > 1 ? "Successful" : "Fail" } }.ToString(),
                ContentType = "application/json",
                StatusCode = result.Count > 1 ? (int)200 : (int)404
            };
        }

    }
}
