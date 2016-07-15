using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.SearchCriterias;
using DAL.Generator;

namespace DAL.Repositories
{
    public class MemoryRepository : IRepository<UserEntity>
    {
        private List<UserEntity> users;
        private IGenerator generator;
        private readonly IFileRepository<SavedEntity> fileRepository;

        public MemoryRepository(IGenerator generator, IFileRepository<SavedEntity> fileRepository)
        {
            this.generator = generator;
            this.fileRepository = fileRepository;
            SavedEntity entity  = fileRepository.GetState();

            users = new List<UserEntity>(entity.Users);
            generator.SetIdPosition(entity.GeneratorPosition);  

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

        ~MemoryRepository(){
            //fileRepository.SaveAll(users);
        }
    }
}
