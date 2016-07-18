using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL.Entities;

namespace DALTests
{
    [TestClass]
    public class UserEntityTest
    {
        [TestMethod]
        public void Equals_TwoSameUserEntities_returnsTrue()
        {
            var visa1 = new VisaRecord() { Country = "Austria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visa2 = new VisaRecord() { Country = "Bulgaria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visaRecords1 = new VisaRecord[]
            {
                visa1,
                visa2
            };
            var visaRecords2 = new VisaRecord[] {
                visa1,
                visa2
            };
            var userEntity1 = new UserEntity()
            {
                Id = 4,
                FirstName = "Ivan",
                LastName = "Petrov",
                Gender = UserGender.male,
                VisaRecords = visaRecords1
            };
            var userEntity2 = new UserEntity()
            {
                Id = 4,
                FirstName = "Ivan",
                LastName = "Petrov",
                Gender = UserGender.male,
                VisaRecords = visaRecords2
            };
            Assert.IsTrue(userEntity1.Equals(userEntity2));
        }
        [TestMethod]
        public void Equals_TwoUserEntitiesWithDifferentId_returnsTrue()
        {
            var visa1 = new VisaRecord() { Country = "Austria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visa2 = new VisaRecord() { Country = "Bulgaria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visaRecords1 = new VisaRecord[]
            {
                visa1,
                visa2
            };
            var visaRecords2 = new VisaRecord[] {
                visa1,
                visa2
            };
            var userEntity1 = new UserEntity()
            {
                Id = 4,
                FirstName = "Ivan",
                LastName = "Petrov",
                Gender = UserGender.male,
                VisaRecords = visaRecords1
            };
            var userEntity2 = new UserEntity()
            {
                Id = 5,
                FirstName = "Ivan",
                LastName = "Petrov",
                Gender = UserGender.male,
                VisaRecords = visaRecords2
            };
            Assert.IsTrue(userEntity1.Equals(userEntity2));
        }
        [TestMethod]
        public void Equals_TwoUserEntitiesWithDifferentVisas_returnFalse()
        {
            var visa1 = new VisaRecord() { Country = "Austria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visa2 = new VisaRecord() { Country = "Bulgaria", EndDate = DateTime.Now, StartDate = DateTime.Now };
            var visaRecords1 = new VisaRecord[]
            {
                visa1,
                visa2
            };
            var visaRecords2 = new VisaRecord[] {
                visa1
            };
            var userEntity1 = new UserEntity()
            {
                Id = 4,
                FirstName = "Ivan",
                LastName = "Petrov",
                Gender = UserGender.male,
                VisaRecords = visaRecords1
            };
            var userEntity2 = new UserEntity()
            {
                Id = 5,
                FirstName = "Ivan",
                LastName = "Petrov",
                Gender = UserGender.male,
                VisaRecords = visaRecords2
            };
            Assert.IsFalse(userEntity1.Equals(userEntity2));
        }    
    }
}
