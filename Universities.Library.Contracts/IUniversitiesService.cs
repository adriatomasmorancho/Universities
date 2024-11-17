using Universities.Library.Contracts.DTOs;

namespace Universities.Library.Contracts
{
    public interface IUniversitiesService
    {
        Task<MigrateAllRsDto> MigrateAllAsync();

    }
}
