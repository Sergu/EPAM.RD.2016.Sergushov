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
    public class SlaveService : MarshalByRefObject, IService<UserBll>, INotifiedService<UserBll>
    {
        private List<UserBll> users;

        public SlaveService()
        {
            users = new List<UserBll>();
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("slave service created. domain : {0}",AppDomain.CurrentDomain.FriendlyName);
            var domain = AppDomain.CurrentDomain;
        }
        public int Add(UserBll entity)
        {
            if (BllLogger.IsLogged)
                BllLogger.Instance.Warn("slave service try to add user entity");
            throw new Exception();
        }
        public void Delete(int id)
        {
            if (BllLogger.IsLogged)
                BllLogger.Instance.Warn("slave service try to delete user entity");
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
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("master service searched users : {0}", suitableUsers.Count());
            return suitableUsers;
        }
        public void NotifyAdd(UserBll user)
        {
            users.Add(user);
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("slave service notified add user: {0}",user.Id);
        }
        public void NotifyDelete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            users.Remove(user);
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("slave service notified delete user: {0}", id);
        }
        public void Init(IEnumerable<UserBll> users)
        {
            this.users = new List<UserBll>(users);
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("slave service initialized");
        }
    }
}
