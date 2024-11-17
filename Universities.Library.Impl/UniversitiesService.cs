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

        #region ListAll
        public ListAllRsDto ListAllAsync()
        {
            ListAllRsDto result = new();

            List<University> resultFromRepository =
                        _universitiesDBRepository.GetAll();
            result.data = MapWebApiEntityListToDtoList(resultFromRepository);

            return result;
        }

        private static List<UniversityNameAndCountryDto> MapWebApiEntityListToDtoList(
            List<University> dbEntityList)
        {
            return dbEntityList.Select(x => new UniversityNameAndCountryDto()
            {
                Name = x.Name,
                Country = x.Country
            }).ToList();
        }
        #endregion

        #region FilterByName
        public List<UniversityNameAndWebpageListDto> FilterByName(string name)
        {
            List<University> resultFromRepository =
                        _universitiesDBRepository.GetByName(name);

            return MapDbEntityListToUniversityNameAndWebpageDtoList(resultFromRepository);
        }

        private static List<UniversityNameAndWebpageListDto> MapDbEntityListToUniversityNameAndWebpageDtoList(
            List<University> dbEntityList)
        {
            return dbEntityList.Select(x => new UniversityNameAndWebpageListDto()
            {
                Name = x.Name,
                WebpageList = x.WebPages.Select(x => x.WebPageName).ToList()
            }).ToList();
        }
        #endregion

        #region FilterByAlphaTwoCode
        public List<UniversityNameDto> FilterByAlphaTwoCode(string alphaTwoCode)
        {
            List<University> resultFromRepository =
                        _universitiesDBRepository.GetByAlphaTwoCode(alphaTwoCode);
            return MapDbEntityListToUniversityNameDtoList(resultFromRepository);
        }

        private static List<UniversityNameDto> MapDbEntityListToUniversityNameDtoList(
            List<University> dbEntityList)
        {
            return dbEntityList.Select(x => new UniversityNameDto()
            {
                Name = x.Name
            }).ToList();
        }
        #endregion

    }
}

