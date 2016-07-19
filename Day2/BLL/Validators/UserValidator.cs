    using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    [Serializable]
    public class UserValidator : IValidator<UserBll>
    {
        public bool Validate(UserBll entity)
        {
            if (!ReferenceEquals(entity.VisaRecords, null))
            {
                if ((entity.FirstName == "") || (entity.LastName == ""))
                    return false;
                return true;
            }
            return false;
        }
    }
}
