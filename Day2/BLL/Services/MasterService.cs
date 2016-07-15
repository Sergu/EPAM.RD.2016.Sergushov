using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;
using DAL.SearchCriterias;
using DAL.Repositories;
using BLL.Validators;
using DAL.Entities;
using BLL.Mappers;

namespace BLL.Services
{
    public class MasterService : IService<UserBll>
    {
        private readonly IRepository<UserEntity> repository;
        private IValidator<UserBll> userValidator;
        private IEnumerable<INotifyingService> slaveServices; 

        public MasterService(IRepository<UserEntity> repository,IValidator<UserBll> validator,IEnumerable<INotifyingService> slaveServices)
        {
            this.repository = repository;
            this.userValidator = validator;
            this.slaveServices = slaveServices;
        }

        public int Add(UserBll entity)
        {
            if (userValidator.Validate(entity))
            {
                var userId = repository.Add(entity.ToUserEntity());
                foreach (var slave in slaveServices)
                    slave.Notify(); //????
                return userId;
            }
            else
                throw new Exception();
        }
        public void Delete(int id)
        {
            repository.Delete(id);
        }
        public IEnumerable<UserBll> Search(ISearchCriteria criteria)
        {
            return repository.Search(criteria).Select(user => user.ToBllUser());
        }

    }
}
