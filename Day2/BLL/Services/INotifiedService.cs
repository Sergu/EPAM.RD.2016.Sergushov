using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface INotifiedService<T>
    {
        void NotifyAdd(T user);
        void NotifyDelete(int id);
        void Init(IEnumerable<T> users);
    }
}
