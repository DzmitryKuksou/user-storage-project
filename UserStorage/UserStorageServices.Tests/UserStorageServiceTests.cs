using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameConsistsOfWhiteSpaces_ExceptionThrown()
        {
            //// Arrange
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            //// Act
            storageLog.Add(new User
            {
                FirstName = "    "
            });

            //// Assert - [ExpectedException]
        }

        public void Add_UserLastNameConsistsOfWhiteSpaces_ExceptionThrown()
        {
            //// Arrange
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            //// Act
            storageLog.Add(new User
            {
                LastName = "    "
            });

            //// Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserAgeIsNegative_ExceptionThrown()
        {
            //// Arrange
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            ////Act
            storageLog.Add(new User
            {
                FirstName = "Alex",
                LastName = "Johnson",
                Age = -1
            });

            //// Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);

            // Act
            storageLog.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            // Act
            storageLog.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_WithoutArguments_NothingHappen()
        {
            // Arrange
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            // Act
            storageLog.Add(new User
            {
                FirstName = "Alex",
                LastName = "Usov",
                Age = 21,
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void SearchByFirstName_User()
        {
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = 20
            };
            storageLog.Add(user);
            Assert.AreEqual(storageLog.SearchByFirstName(user.FirstName).FirstOrDefault(), user);
        }

        [TestMethod]
        public void SearchByLastName_User()
        {
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = 20
            };
            storageLog.Add(user);
            Assert.AreEqual(storageLog.SearchByLastName(user.LastName).FirstOrDefault(), user);
        }

        [TestMethod]
        public void SearchByAge_User()
        {
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = 20
            };
            storageLog.Add(user);
            Assert.AreEqual(storageLog.SearchByAge(user.Age).FirstOrDefault(), user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByAge_Exception()
        {
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = -20
            };
            storageLog.Add(user);
            Assert.AreEqual(storageLog.SearchByAge(user.Age).FirstOrDefault(), user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByName_Exception()
        {
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            var user = new User()
            {
                LastName = "Big",
                Age = 20
            };
            storageLog.Add(user);
            Assert.AreEqual(storageLog.SearchByAge(user.Age).FirstOrDefault(), user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByName_Exception2()
        {
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            var user = new User()
            {
                FirstName = "Yan",
                Age = -20
            };
            storageLog.Add(user);
            Assert.AreEqual(storageLog.SearchByAge(user.Age).FirstOrDefault(), user);
        }

        [TestMethod]
        public void SearchByNameAndAge()
        {
            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = 20
            };
            var user1 = new User()
            {
                FirstName = "Yan",
                LastName = "Little",
                Age = 30
            };
            var slaveNode1 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveNode2 = new UserStorageServiceSlave(new UserId(), new CompositeValidator());
            var slaveServiceCollection = new List<IUserStorageService>() { slaveNode1, slaveNode2 };

            var storage = new UserStorageServiceMaster(new UserId(), new CompositeValidator(), slaveServiceCollection);
            var storageLog = new UserStorageServiceLog(storage);
            storageLog.Add(user);
            storageLog.Add(user1);
            Assert.AreEqual(storageLog.SearchByPredicate(u => u.FirstName == "Yan" && u.Age == 30).FirstOrDefault(), user1);
        }
    }
}
