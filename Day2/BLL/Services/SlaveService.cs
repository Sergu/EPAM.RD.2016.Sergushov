using BLL.Entities;
using DAL.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mappers;

namespace BLL.Services
{
    public class SlaveService : IService<UserBll>, INotifyingService
    {
        private IEnumerable<UserBll> users;

        public SlaveService(IEnumerable<UserBll> users)
        {
            this.users = users;
        }

        public int Add(UserBll entity)
        {
            throw new Exception();
        }
        public void Delete(int id)
        {
            throw new Exception();
        }
        public IEnumerable<UserBll> Search(ISearchCriteria criteria)
        {
            List<UserBll> suitableUsers = new List<UserBll>();
            foreach(var user in users)
            {
                if(criteria.IsSuitable(user.ToUserEntity()))
                {
                    suitableUsers.Add(user);
                }
            }
            return suitableUsers;
        }
        public void Notify(IEnumerable<UserBll> users)
        {
            this.users = users;
        }
    }
}
