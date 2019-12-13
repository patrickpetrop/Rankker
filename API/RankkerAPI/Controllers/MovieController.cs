using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RankkerCommon.Models;

namespace RankkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Comment to add in info for MovieGenres

        [HttpGet]
        public async Task<IActionResult> CreateInitalMovie()
        {
            var jwtSecretKey = _configuration.GetSection("JwtSecretKey").Value;


            var tmdbApiKey = _configuration.GetSection("TMDB_API_Key").Value;


            var conString = _configuration.GetConnectionString("DefaultConnection");

            return Json(new {jwtSecretKey, tmdbApiKey, conString});



            //            var conString = _configuration.GetConnectionString("DefaultConnection");
            //
            //            var movie = new Movie()
            //            {
            //                Name = "TempName",
            //                Tagline = "tag",
            //                Overview = "over",
            //                TmdbId = 1234,
            //                ImdbId = "tff",
            //                TmdbPosterPath = "fdsa",
            //                TmdbBackdropPath = "wesdfa",
            //                Status = "eee"
            //            };
            //
            //            using (IDbConnection connection = new SqlConnection(conString))
            //            {
            //                //Remove Dapper from RankkerAPI
            //                var insertedId = connection.Query<int>("dbo.Movie_Insert",
            //                    movie, commandType: CommandType.StoredProcedure).Single();
            //            }
            //
            //            return new OkObjectResult("Movie Created");
        }



    }
}