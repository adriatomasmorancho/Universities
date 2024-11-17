using System.Text.Json;
using Universities.Infrastructure.Contracts;
using Universities.Infrastructure.Contracts.Entities;

namespace Universities.Infrastructure.Impl
{
    public class UniversitiesApiRepository : IUniversitiesApiRepository
    {
        public async Task<List<UniversityWebApiEntity>?> GetAllAsync()
        {
            using HttpClient client = new();


            HttpResponseMessage dataFromWebApi = await client.GetAsync("http://universities.hipolabs.com/search");
            string dataAsString = await dataFromWebApi.Content.ReadAsStringAsync();
            List<UniversityWebApiEntity>? dataDeserialized =
                JsonSerializer.Deserialize<List<UniversityWebApiEntity>>(dataAsString);

            return dataDeserialized;
        }
    }
}
