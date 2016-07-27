using BLL.Entities;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class BllMapper
    {
        public static UserBll ToBllUser(this UserEntity entity)
        {
            return new UserBll
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                VisaRecords = entity.VisaRecords
            };
        }
        public static UserEntity ToUserEntity(this UserBll entity)
        {
            return new UserEntity
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                VisaRecords = entity.VisaRecords
            };
        }
    }
}
