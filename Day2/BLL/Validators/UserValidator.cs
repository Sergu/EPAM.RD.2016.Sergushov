using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators
{
    public class UserValidator : IValidator<UserBll>
    {
        public bool Validate(UserBll entity)
        {
            if (!ReferenceEquals(entity.VisaRecords, null))
                return true;
            return false;
        }
    }
}
