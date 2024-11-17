using Universities.Library.Contracts.DTOs;

namespace Universities.Library.Contracts
{
    public interface IUniversitiesService
    {
        Task<MigrateAllRsDto> MigrateAllAsync();

        ListAllRsDto ListAllAsync();

        List<UniversityNameAndWebpageListDto> FilterByName(string name);

        List<UniversityNameDto> FilterByAlphaTwoCode(string alphaTwoCode);

    }
}
