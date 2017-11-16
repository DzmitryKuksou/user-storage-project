using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public abstract class UserStorageServiceBase : IUserStorageService, INotificationSubscriber
    {
        /// <summary>
        /// repository
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// c-or
        /// </summary>
        protected UserStorageServiceBase(IUserRepository userRepository)
        {

        }

        public abstract UserStorageServiceMode ServiceMode { get; }

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => userRepository.Count;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public virtual void Add(User user)
        {
            userRepository.Set(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public virtual bool Remove(User user)
        {
            return userRepository.Delete(user);
        }

        /// <summary>
        /// Search by name
        /// </summary>
        /// <param name="firstName">name of user</param>
        /// <returns>users</returns>
        public virtual IEnumerable<User> SearchByFirstName(string firstName)
        {
            return SearchByPredicate(u => u.FirstName == firstName);
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="lastName">last name of user</param>
        /// <returns></returns>
        public virtual IEnumerable<User> SearchByLastName(string lastName)
        {
            return SearchByPredicate(u => u.LastName == lastName);
        }

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="age">age/param>
        /// <returns></returns>
        public virtual IEnumerable<User> SearchByAge(int age)
        {
            return SearchByPredicate(u => u.Age == age);
        }

        /// <summary>
        /// Search By Predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns></returns>
        public virtual IEnumerable<User> SearchByPredicate(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException();
            }

            var current = userRepository.Query(predicate);

            return current;
        }

        public virtual void UserAdded(User user)
        {
            Add(user);
        }

        public virtual void UserRemoved(User user)
        {
            Remove(user);
        }
    }
}
