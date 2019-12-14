using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RankkerAPI.Helpers;
using RankkerCommon.DataAccess;
using RankkerCommon.TMDB;

namespace RankkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopulateController : ControllerBase
    {

        private readonly ILogger<PopulateController> _logger;
        private readonly string _tmdbApiKey;
        private readonly string _connectionString;

        public PopulateController(IConfiguration configuration, ILogger<PopulateController> logger)
        {
            _logger = logger;
            _tmdbApiKey = GetConfigurationValues.GetTmdbApiKey(configuration);
            _connectionString = GetConfigurationValues.GetConnectionString(configuration);
        }

        [HttpGet("moviegenre")]
        public async Task<IActionResult> PopulateMovieGenres()
        {
            var result = await TmdbMovieService.GetListOfMovieGenres(_tmdbApiKey);

            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                foreach (var movieGenre in result)
                {
                    try
                    {
                        //Remove Dapper from RankkerAPI
                        connection.Query<int>(StoredProcedures.MOVIEGENRE_INSERT,
                            movieGenre, commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Error while inserting movie genre with code " + e.Message);
                    }
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