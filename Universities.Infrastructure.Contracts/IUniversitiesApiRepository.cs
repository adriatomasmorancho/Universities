using Universities.Infrastructure.Contracts.Entities;

namespace Universities.Infrastructure.Contracts
{
    public interface IUniversitiesApiRepository
    {
        Task<List<UniversityWebApiEntity>?> GetAllAsync();


    }
}
