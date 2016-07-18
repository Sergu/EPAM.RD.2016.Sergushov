using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Repositories;
using DAL.Entities;
using DAL.Generator;
using CustomNumberGenerators;
using DAL.SearchCriterias;

namespace DALTests
{
    [TestClass]
    public class MemoryRepositoryTest
    {
        public MemoryRepository CreateMemoryRepository()
        {
            IdGenerator generator = new IdGenerator(new SimpleNumberGenerator());
            return new MemoryRepository(generator);
        }
        public UserEntity CreateUserEntity(string firstName,string lastName)
        {
            var visa1 = new VisaRecord() { Country = "Austria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visa2 = new VisaRecord() { Country = "Bulgaria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visaRecords1 = new VisaRecord[]
            {
                visa1,
                visa2
            };
            return new UserEntity()
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = UserGender.male,
                VisaRecords = visaRecords1
            };
        }
        [TestMethod]
        public void Add_userEntityAddToRepository_nextAddReturnsBiggerIdThanOnThePreviousAdditing()
        {
            bool IsBigger = true;
            var repository = CreateMemoryRepository();
            var users = new UserEntity[]
            {
                CreateUserEntity("Ivan", "Petrov"),
                CreateUserEntity("i", "Petrov"),
                CreateUserEntity("v", "Petrov"),
                CreateUserEntity("q", "Petrov"),
                CreateUserEntity("e", "Petrov"),
                CreateUserEntity("t", "Petrov")
            };
            var listId = new List<int>();
            foreach(var user in users)
            {
                listId.Add(repository.Add(user));
            }

            var prevId = 0;
            foreach(var id in listId)
            {
                if (id > prevId)
                    prevId = id;
                else
                    IsBigger = false;
            }
            Assert.IsTrue(IsBigger);
        }
        [TestMethod]
        public void Add_checkMemoryRepositoryCountAfterAdding_returnsAddedEntitiesCount()
        {
            var repository = CreateMemoryRepository();
            var users = new UserEntity[]
            {
                CreateUserEntity("Ivan", "Petrov"),
                CreateUserEntity("i", "Petrov"),
                CreateUserEntity("v", "Petrov"),
                CreateUserEntity("q", "Petrov"),
                CreateUserEntity("e", "Petrov"),
                CreateUserEntity("t", "Petrov")
            };
            foreach (var user in users)
            {
                repository.Add(user);
            }
            Assert.AreEqual(users.Count(),repository.GetSavedState().Users.Count());
        }
        [TestMethod]
        public void Delete_checkMemoryRepositoryAfterRemoving_returnsUsersWithoutRemovedUser()
        {
            var repository = CreateMemoryRepository();
            var removedEntity = CreateUserEntity("v", "Petrov");
            var users = new UserEntity[]
            {
                CreateUserEntity("Ivan", "Petrov"),
                CreateUserEntity("i", "Petrov"),
                removedEntity,
                CreateUserEntity("q", "Petrov"),
                CreateUserEntity("e", "Petrov"),
                CreateUserEntity("t", "Petrov")
            };
            int removedId = 0;
            foreach (var user in users)
            {
                if(user.FirstName==removedEntity.FirstName)
                    removedId = repository.Add(user);
                else
                    repository.Add(user);
            }
            repository.Delete(removedId);
            var memoryUsers = repository.GetSavedState().Users;
            var result = memoryUsers.FirstOrDefault(u => u.Equals(removedEntity));
            Assert.IsNull(result);
        }
        [TestMethod]
        public void Search_searchByFirstName_checkSerarchedEntityWithExcpected()
        {
            var repository = CreateMemoryRepository();
            var searchedEntity1 = CreateUserEntity("v", "Petrov");
            var searchedEntity2 = CreateUserEntity("v", "Peov");
            var expectedResult = new List<UserEntity>(new UserEntity[] { searchedEntity1, searchedEntity2 });
            var users = new UserEntity[]
            {
                CreateUserEntity("Ivan", "Petrov"),
                CreateUserEntity("i", "Petrov"),
                searchedEntity1,
                searchedEntity2,
                CreateUserEntity("e", "Petrov"),
                CreateUserEntity("t", "Petrov")
            };
            foreach (var user in users)
            {
                repository.Add(user);
            }
            var criteria = new FirstNameCriteria("v");
            var searchedResult = new List<UserEntity>(repository.Search(criteria));
            CollectionAssert.AreEqual(searchedResult, expectedResult);
        }
    }
}
