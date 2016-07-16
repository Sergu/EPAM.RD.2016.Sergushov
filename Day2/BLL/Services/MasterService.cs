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
using DAL.Exceptions;

namespace BLL.Services
{
    public class MasterService : IService<UserBll>
    {
        private readonly IRepository<UserEntity> repository;
        private IValidator<UserBll> userValidator;
        private IEnumerable<INotifiedService<UserBll>> slaveServices;
        private IFileRepository<SavedEntity> fileRepository;

        public MasterService(IRepository<UserEntity> repository,IValidator<UserBll> validator,IEnumerable<INotifiedService<UserBll>> slaveServices,IFileRepository<SavedEntity> fileRepository)
        {
            this.repository = repository;
            this.userValidator = validator;
            this.slaveServices = slaveServices;
            this.fileRepository = fileRepository;
            SavedEntity savedState = new SavedEntity();
            try
            {
                savedState = fileRepository.GetState();
            }catch(XmlException ex)
            {
                savedState.Users = new List<UserEntity>();
                savedState.GeneratorPosition = 0;
            }
            this.repository.Update(savedState);
            foreach(var slave in slaveServices)
            {
                slave.Init(savedState.Users.Select(u => u.ToBllUser()));
            }
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("master service created");
        }

        public int Add(UserBll entity)
        {
            if (userValidator.Validate(entity))
            {
                var userId = repository.Add(entity.ToUserEntity());
                if (BllLogger.IsLogged)
                    BllLogger.Instance.Trace("master service notify slaves to add user : {0}", entity.Id);
                foreach (var slave in slaveServices)
                {
                    slave.NotifyAdd(entity);
                }
                return userId;
            }
            else
                throw new Exception();
        }
        public void Delete(int id)
        {
            repository.Delete(id);
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("master service notify slaves to delete user : {0}", id);
            foreach (var slave in slaveServices)
                slave.NotifyDelete(id);
        }
        public IEnumerable<UserBll> Search(ISearchCriteria criteria)
        {
            var res = repository.Search(criteria).Select(user => user.ToBllUser());
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("master service searched users : {0}", res.Count());
            return res;
        }
        public void SaveServiceState()
        {
            var savedState = repository.GetSavedState();

            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("master service save state : users {0} , generator {1}", savedState.Users.Count,savedState.GeneratorPosition);
            fileRepository.SaveState(savedState);
        }
        public void UpdateServiceState()
        {
            SavedEntity entity = null;
            try
            {
                entity = fileRepository.GetState();
            }
            catch (XmlException)
            {
                entity.Users = new List<UserEntity>();
                entity.GeneratorPosition = 0;
            }
            if (BllLogger.IsLogged)
                BllLogger.Instance.Trace("master service update state : users {0} , generator {1}", entity.Users.Count, entity.GeneratorPosition);
            repository.Update(entity);
        }
    }
}
