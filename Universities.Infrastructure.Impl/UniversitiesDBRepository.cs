using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universities.Infrastructure.Contracts;
using Universities.Infrastructure.Contracts.EntitiesDB;
using Universities.Infrastructure.Impl.DbContext;

namespace Universities.Infrastructure.Impl
{
    public class UniversitiesDBRepository : IUniversitiesDBRepository
    {

        private readonly UniversityDBContext _dbContext;

        public UniversitiesDBRepository(UniversityDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveAll(List<University> dataToSave)
        {
            _dbContext.Universities.AddRange(dataToSave);
            _dbContext.SaveChanges();
        }
    }
}
