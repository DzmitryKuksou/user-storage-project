﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            //// Act
            userStorageService.Add(new User
            {
                FirstName = "    "
            });

            //// Assert - [ExpectedException]
        }

        public void Add_UserLastNameConsistsOfWhiteSpaces_ExceptionThrown()
        {
            //// Arrange
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            //// Act
            userStorageService.Add(new User
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
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            ////Act
            userStorageService.Add(new User
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
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            // Act
            userStorageService.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_WithoutArguments_NothingHappen()
        {
            // Arrange
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            // Act
            userStorageService.Add(new User
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
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = 20
            };
            userStorageService.Add(user);
            Assert.AreEqual(userStorageService.SearchByFirstName(user.FirstName).FirstOrDefault(), user);
        }

        [TestMethod]
        public void SearchByLastName_User()
        {
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = 20
            };
            userStorageService.Add(user);
            Assert.AreEqual(userStorageService.SearchByLastName(user.LastName).FirstOrDefault(), user);
        }

        [TestMethod]
        public void SearchByAge_User()
        {
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = 20
            };
            userStorageService.Add(user);
            Assert.AreEqual(userStorageService.SearchByAge(user.Age).FirstOrDefault(), user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByAge_Exception()
        {
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            var user = new User()
            {
                FirstName = "Yan",
                LastName = "Big",
                Age = -20
            };
            userStorageService.Add(user);
            Assert.AreEqual(userStorageService.SearchByAge(user.Age).FirstOrDefault(), user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByName_Exception()
        {
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            var user = new User()
            {
                LastName = "Big",
                Age = 20
            };
            userStorageService.Add(user);
            Assert.AreEqual(userStorageService.SearchByAge(user.Age).FirstOrDefault(), user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SearchByName_Exception2()
        {
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());

            var user = new User()
            {
                FirstName = "Yan",
                Age = -20
            };
            userStorageService.Add(user);
            Assert.AreEqual(userStorageService.SearchByAge(user.Age).FirstOrDefault(), user);
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
            var userStorageService = new UserStorageService(new UserId(), new CompositeValidator());
            userStorageService.Add(user);
            userStorageService.Add(user1);
            Assert.AreEqual(userStorageService.SearchByPredicate(u => u.FirstName == "Yan" && u.Age == 30).FirstOrDefault(), user1);
        }
    }
}
