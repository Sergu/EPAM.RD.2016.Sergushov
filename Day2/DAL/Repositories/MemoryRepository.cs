using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.SearchCriterias;
using DAL.Generator;
using DAL.Exceptions;

namespace DAL.Repositories
{
    [Serializable]
    public class MemoryRepository : IRepository<UserEntity>
    {
        private List<UserEntity> users;
        private IGenerator generator;

        public MemoryRepository(IGenerator generator)
        {
            this.generator = generator;
            users = new List<UserEntity>();
        }

        public int Add(UserEntity entity)
        {
            entity.Id = generator.GenerateId();
            users.Add(entity);
            return entity.Id;
        }
        public void Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            users.Remove(user);
        }
        public IEnumerable<UserEntity> Search(ISearchCriteria criteria)
        {
            List<UserEntity> suitableUsers = new List<UserEntity>();
            foreach(var user in users)
            {
                if (criteria.IsSuitable(user))
                {
                    suitableUsers.Add(user);
                }
            }
            return suitableUsers;
        }
        public SavedEntity GetSavedState()
        {
            var generatorPosition = generator.GetCurrentPosition();
            return new SavedEntity
            {
                GeneratorPosition = generatorPosition,
                Users = users
            };
        }
        public void Update(SavedEntity entity)
        {
            generator.SetIdPosition(entity.GeneratorPosition);
            users = entity.Users;
        }
    }
}
