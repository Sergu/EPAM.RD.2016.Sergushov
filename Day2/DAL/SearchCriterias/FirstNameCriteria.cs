using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.SearchCriterias
{
    [Serializable]
    public class FirstNameCriteria : ISearchCriteria
    {
        private string Name;
        public FirstNameCriteria(string firstName)
        {
            this.Name = firstName;
        }
        public bool IsSuitable(UserEntity entity)
        {
            if (entity.FirstName == Name)
                return true;
            return false;
        }
    }
}
