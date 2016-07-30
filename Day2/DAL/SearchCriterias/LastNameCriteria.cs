using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.SearchCriterias
{
    [Serializable]
    public class LastNameCriteria : ISearchCriteria
    {
        private string lastName;
        public LastNameCriteria(string lastName)
        {
            this.lastName = lastName;
        }
        public bool IsSuitable(UserEntity entity)
        {
            if (entity.LastName == lastName)
                return true;
            return false;
        }
    }
}
