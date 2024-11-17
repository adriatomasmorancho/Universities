using System.Text.Json;
using Universities.Infrastructure.Contracts;
using Universities.Infrastructure.Contracts.Entities;
using Universities.Infrastructure.Contracts.EntitiesDB;
using Universities.Infrastructure.Impl;
using Universities.Library.Contracts;
using Universities.Library.Contracts.DTOs;
using Universities.XCutting.Enums;

namespace Universities.Library.Impl
{
    public class UniversitiesService : IUniversitiesService
    {
        private readonly IUniversitiesApiRepository _universitiesApiRepository;
        private readonly IUniversitiesDBRepository _universitiesDBRepository;

        public UniversitiesService(
            IUniversitiesApiRepository universitiesApiRepository,
            IUniversitiesDBRepository universitiesDbRepository)
        {
            _universitiesApiRepository = universitiesApiRepository;
            _universitiesDBRepository = universitiesDbRepository;
        }
        #region MigrateAll
        public async Task<MigrateAllRsDto> MigrateAllAsync()
        {
            MigrateAllRsDto result = new();

            try
            {
                List<UniversityWebApiEntity>? resultFromRepository =
                        await _universitiesApiRepository.GetAllAsync();

                if (resultFromRepository == null)
                {
                    result.errors ??= new();
                    result.errors.Add(ErrorsEnum.WebApiDataDeserializationReturnsNullError);
                }
                else
                {
                    List<University> dbEntityList =
                        MapWebApiEntityListToDbEntityList(resultFromRepository);
                    try
                    {
                        _universitiesDBRepository.SaveAll(dbEntityList);
                    }
                    catch (Exception)
                    {
                        result.errors ??= new();
                        result.errors.Add(ErrorsEnum.DbSaveError);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException ||
                    ex is HttpRequestException ||
                    ex is TaskCanceledException)
                {
                    result.errors ??= new();
                    result.errors.Add(ErrorsEnum.WebApiConnectionError);
                }
                else if (ex is ArgumentNullException ||
                    ex is JsonException ||
                    ex is NotSupportedException)
                {
                    result.errors ??= new();
                    result.errors.Add(ErrorsEnum.WebApiDataDeserializationExceptionError);
                }
            }

            return result;
        }

        private static List<University> MapWebApiEntityListToDbEntityList(
        List<UniversityWebApiEntity> webApiEntityList)
        {
            List<University> dbEntityList = new();
            for (int i = 0; i < webApiEntityList.Count; i++)
            {
                UniversityWebApiEntity currentWebApiEntity = webApiEntityList[i];
                University rowToAdd = new()
                {
                    IdFromWebApi = i,
                    Name = currentWebApiEntity.Name,
                    AlphaTwoCode = currentWebApiEntity.Code,
                    StateProvince = currentWebApiEntity.StateProvince,
                    Country = currentWebApiEntity.Country,
                    Domains = currentWebApiEntity.DomainList?.Select(domainName =>
                    new Domain
                    {
                        DomainName = domainName
                    })
                    .ToList() ?? new List<Domain>(),
                    WebPages = currentWebApiEntity.WebPageList?.Select(webPageName =>
                    new WebPage
                    {
                        WebPageName = webPageName
                    })
                    .ToList() ?? new List<WebPage>()
                };
                dbEntityList.Add(rowToAdd);
            }

            return dbEntityList;
        }
        #endregion
    }
}

