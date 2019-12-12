using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RankkerCommon.DataAccess;
using RankkerCommon.DTOs;
using RankkerCommon.ModelDataAccess;
using RankkerCommon.Models;

namespace RankkerCommon.Tests.ModelDataAccessTests
{
    [TestFixture()]
    public class MovieDataTests
    {

        private string _connectionStringName;
        private string _connectionString;

        [SetUp]
        public void Setup()
        {
            _connectionStringName = "ConnectionStringName";
            _connectionString = "ConnectionString";
        }


        [Test]
        public void GetAllMovies_CalledWithConnectionString_ReturnsListOfMovies()
        {
            var movieList = new List<Movie>()
            {
                new Movie()
                {
                    Id = 1
                },
                new Movie()
                {
                    Id = 2
                }
            };

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c.GetConnectionString(It.IsAny<string>()))
                .Returns(_connectionString);

            var sqlDataAccessMoq = new Mock<ISqlDataAccess>();

            var movieData = new MovieData(configMock.Object, sqlDataAccessMoq.Object);

            sqlDataAccessMoq.Setup(c =>
                c.LoadDataInTransaction<Movie, Empty>(It.IsAny<string>(), It.IsAny<Empty>() )
                    ).Returns(movieList);


            var returnedMovies = movieData.GetAllMovies(_connectionStringName);

            Assert.AreEqual(movieList[0].Id, returnedMovies[0].Id);

            configMock.Verify(c => c.GetConnectionString(It.Is<string>(d => d.Equals(_connectionStringName))), Times.Once);
            sqlDataAccessMoq.Verify(c => c.StartTransaction(It.Is<string>(d => d.Equals(_connectionString))), Times.Once());

            

        }


    }
}