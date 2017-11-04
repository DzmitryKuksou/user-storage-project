using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        /// <summary>
        /// new identification number
        /// </summary>
        private IGeneratorId newId;

        /// <summary>
        /// list of users
        /// </summary>
        private List<User> users;

        /// <summary>
        /// c-or
        /// </summary>
        public UserStorageService()
        {
            users = new List<User>();
            newId = new UserId();
        }

        /// <summary>
        /// field log
        /// </summary>
        public bool IsLoggingEnabled { get; set; }

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
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName)) 
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(user));
            }

            if (user.Age <= 0)
            {
                throw new ArgumentException("Age less than zero", nameof(user));
            }

            user.Id = newId.Generate();

            users.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove(User user)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("FirstName is null or empty, or whitespace", nameof(user));
            }

            if (user.Age <= 0)
            {
                throw new ArgumentException("Age less than zero", nameof(user));
            }

            users.Remove(user);
        }

        /// <summary>
        /// Search by name
        /// </summary>
        /// <param name="firstName">name of user</param>
        /// <returns>users</returns>
        public IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByFirstName() method is called.");
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("FirstName is null or empty, or whitespace", nameof(firstName));
            }

            return SearchByPredicate(u => u.FirstName == firstName);
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="lastName">last name of user</param>
        /// <returns></returns>
        public IEnumerable<User> SearchByLastName(string lastName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByLastName() method is called.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("FirstName is null or empty, or whitespace", nameof(lastName));
            }

            return SearchByPredicate(u => u.LastName == lastName);
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="age">age/param>
        /// <returns></returns>
        public IEnumerable<User> SearchByAge(int age)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("SearchByAge() method is called.");
            }

            if (age <= 0)
            {
                throw new ArgumentException("FirstName is null or empty, or whitespace", nameof(age));
            }

            return SearchByPredicate(u => u.Age == age);
        }

        /// <summary>
        /// Search By Predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public IEnumerable<User> SearchByPredicate(Predicate<User> predicate)
        {
            if (IsLoggingEnabled)
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
