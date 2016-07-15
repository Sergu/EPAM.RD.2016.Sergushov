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
    public class SlaveService : IService<UserBll>, INotifiedService<UserBll>
    {
        private List<UserBll> users;

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
        public void NotifyAdd(UserBll user)
        {
            users.Add(user);
        }
        public void NotifyDelete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            users.Remove(user);
        }
        public void Init(IEnumerable<UserBll> users)
        {
            this.users = new List<UserBll>(users);
        }
    }
}
