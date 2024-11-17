using Microsoft.EntityFrameworkCore;
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
        public List<University> GetAll()
        {
            return _dbContext.Universities.ToList();
        }

        public List<University> GetByName(string name)
        {
            return _dbContext.Universities
                .Include(x => x.WebPages)
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        public List<University> GetByAlphaTwoCode(string alphaTwoCode)
        {
            return _dbContext.Universities
                .Where(x => x.AlphaTwoCode.ToLower().Contains(alphaTwoCode.ToLower()))
                .ToList();
        }
    }
}
