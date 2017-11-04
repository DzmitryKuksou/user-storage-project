using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService
    {
        /// <summary>
        /// field validation
        /// </summary>
        private IValidation valid;

        /// <summary>
        /// new identification number
        /// </summary>
        private IGeneratorId newId;

        /// <summary>
        /// list of users
        /// </summary>
        private List<User> users;

        /// <summary>
        /// field log
        /// </summary>
        private readonly BooleanSwitch logging = new BooleanSwitch("enable for logging", "managed from app.config");

        /// <summary>
        /// c-or
        /// </summary>
        public UserStorageService(IGeneratorId newId, IValidation valid)
        {
            users = new List<User>();
            this.newId = newId;
            this.valid = valid;
        }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => users.Count;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            valid.Validate(user);

            user.Id = newId.Generate();

            users.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public bool Remove(User user)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            valid.Validate(user);

            return users.Remove(user);
        }

        /// <summary>
        /// Search by name
        /// </summary>
        /// <param name="firstName">name of user</param>
        /// <returns>users</returns>
        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByFirstName() method is called.");
            }

            valid.Validate(firstName);

            return SearchByPredicate(u => u.FirstName == firstName);
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="lastName">last name of user</param>
        /// <returns></returns>
        public IEnumerable<User> SearchByLastName(string lastName)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByLastName() method is called.");
            }

            valid.Validate(lastName);

            return SearchByPredicate(u => u.LastName == lastName);
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="age">age/param>
        /// <returns></returns>
        public IEnumerable<User> SearchByAge(int age)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByAge() method is called.");
            }

            valid.Validate(age);

            return SearchByPredicate(u => u.Age == age);
        }

        /// <summary>
        /// Search By Predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public IEnumerable<User> SearchByPredicate(Predicate<User> predicate)
        {
            if (logging.Enabled)
            {
                Console.WriteLine("SearchByPredicate() method is called.");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var choossingUsers = users.Where(u => predicate(u));

            return choossingUsers;
        }
    }
}
