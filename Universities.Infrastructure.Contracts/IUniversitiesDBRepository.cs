using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universities.Infrastructure.Contracts.EntitiesDB;

namespace Universities.Infrastructure.Contracts
{
    public interface IUniversitiesDBRepository
    {
        void SaveAll(List<University> dataToSave);
        List<University> GetAll();

        List<University> GetByName(string name);

        List<University> GetByAlphaTwoCode(string alphaTwoCode);

    }
}
