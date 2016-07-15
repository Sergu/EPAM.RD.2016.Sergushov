using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.SearchCriterias
{
    public class VisaCriteria : ISearchCriteria
    {
        private VisaRecord visa;
        public VisaCriteria(VisaRecord visa)
        {
            this.visa = visa;
        }
        public bool IsSuitable(UserEntity entity)
        {
            if (entity.VisaRecords.Contains(visa))
                return true;
            return false;
        }
    }
}
