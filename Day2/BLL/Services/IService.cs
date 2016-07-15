using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.SearchCriterias;

namespace BLL.Services
{
    public interface IService<T>
    {
        int Add(T entity);
        void Delete(int id);
        IEnumerable<T> Search(ISearchCriteria criteria);
    }
}
