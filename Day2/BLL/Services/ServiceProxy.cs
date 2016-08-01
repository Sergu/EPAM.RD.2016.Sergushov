using BLL.Entities;
using DAL.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceProxy : IService<UserBll>
    {
        private MasterService master { get; set; }
        private List<IService<UserBll>> slaves { get; set; }
        public ServiceProxy(MasterService master,IEnumerable<IService<UserBll>> slaves)
        {
            this.master = master;
            this.slaves = new List<IService<UserBll>>(slaves);
        }
        public int Add(UserBll user)
        {
            return master.Add(user);
        }
        public void Delete(int id)
        {
            master.Delete(id);
        }
        public IEnumerable<UserBll> Search(ISearchCriteria criteria)
        {
            var serviceId = DateTime.Now.Second % (slaves.Count() + 1);
            if (serviceId == 0)
                return master.Search(criteria);
            else
                return slaves[serviceId].Search(criteria);
        }
        public void Save()
        {
            master.SaveServiceState();
        }
    }
}
