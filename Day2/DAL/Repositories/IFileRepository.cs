using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IFileRepository<T>
    {
        void SaveAll(IEnumerable<T> entities);
        IEnumerable<T> GetAll();
    }
}
