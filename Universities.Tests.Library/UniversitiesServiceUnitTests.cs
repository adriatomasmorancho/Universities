using Microsoft.EntityFrameworkCore;
using Moq;
using System.Text.Json;
using Universities.Infrastructure.Contracts;
using Universities.Infrastructure.Contracts.Entities;
using Universities.Infrastructure.Contracts.EntitiesDB;
using Universities.Library.Contracts.DTOs;
using Universities.Library.Impl;
using Universities.XCutting.Enums;

namespace Universities.Tests.Library
{
    public class UnitTest1
    {
        #region MigrateAllAsync
        [Fact]
        public async void MigrateAllAsync_WhenNoErrors_ReturnNoErrors()
        {
            // Arrange
            List<UniversityWebApiEntity> simulatedWebApiResponse = new();
            Mock<IUniversitiesApiRepository> mockedWebApiRepository = new();
            mockedWebApiRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(simulatedWebApiResponse);
            Mock<IUniversitiesDBRepository> mockedDbRepository = new();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.Null(result.errors);
        }

        [Fact]
        public async void MigrateAllAsync_WhenApiConnectionError_ReturnApiConnectionError()
        {
            // Arrange
            Mock<IUniversitiesApiRepository> mockedWebApiRepository = new();
            mockedWebApiRepository
                .Setup(x => x.GetAllAsync())
                .Throws<HttpRequestException>();
            Mock<IUniversitiesDBRepository> mockedDbRepository = new();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(ErrorsEnum.WebApiConnectionError, result.errors[0]);
        }

        [Fact]
        public async void MigrateAllAsync_WhenDeserializationThrowsException_ReturnDeserializationExceptionError()
        {
            // Arrange
            Mock<IUniversitiesApiRepository> mockedWebApiRepository = new();
            mockedWebApiRepository
                .Setup(x => x.GetAllAsync())
                .Throws<JsonException>();
            Mock<IUniversitiesDBRepository> mockedDbRepository = new();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(ErrorsEnum.WebApiDataDeserializationExceptionError, result.errors[0]);
        }

        [Fact]
        public async void MigrateAllAsync_WhenDeserializationReturnsNull_ReturnDeserializationNullError()
        {
            // Arrange
            Mock<IUniversitiesApiRepository> mockedWebApiRepository = new();
            Mock<IUniversitiesDBRepository> mockedDbRepository = new();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(ErrorsEnum.WebApiDataDeserializationReturnsNullError, result.errors[0]);
        }

        [Fact]
        public async void MigrateAllAsync_WhenDbConnectionError_ReturnDbConnectionError()
        {
            // Arrange
            List<UniversityWebApiEntity> simulatedWebApiResponse = new();
            Mock<IUniversitiesApiRepository> mockedWebApiRepository = new();
            mockedWebApiRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(simulatedWebApiResponse);

            Mock<IUniversitiesDBRepository> mockedDbRepository = new();
            mockedDbRepository
                .Setup(x => x.SaveAll(It.IsAny<List<University>>()))
                .Throws<DbUpdateException>();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(ErrorsEnum.DbSaveError, result.errors[0]);
        }
        #endregion
    }
}