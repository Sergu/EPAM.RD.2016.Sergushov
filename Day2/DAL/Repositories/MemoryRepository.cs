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
        private readonly IFileRepository<UserEntity> fileRepository;

        public MemoryRepository(IGenerator idGenerator, IFileRepository<UserEntity> fileRepository)
        {
            this.generator = idGenerator;
            this.fileRepository = fileRepository;
            //users = new List<UserEntity>(fileRepository.GetAll());       

        }

        public int Add(UserEntity entity)
        {
            entity.Id = generator.GetId();
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
