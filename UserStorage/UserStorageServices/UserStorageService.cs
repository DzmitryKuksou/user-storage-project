using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace UserStorageServices
{
    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService : IUserStorageService, INotificationSubscriber
    {
        /// <summary>
        /// subscribers
        /// </summary>
        private HashSet<INotificationSubscriber> subscribers;

        /// <summary>
        /// field validation
        /// </summary>
        private IValidator valid;

        /// <summary>
        /// new identification number
        /// </summary>
        private IGeneratorId newId;

        /// <summary>
        /// list of users
        /// </summary>
        private List<User> users;

        /// <summary>
        /// mode
        /// </summary>
        private readonly UserStorageServiceMode mode;

        /// <summary>
        /// slave node
        /// </summary>
        private readonly IList<IUserStorageService> slaveService;

        /// <summary>
        /// field log
        /// </summary>
        private readonly BooleanSwitch logging = new BooleanSwitch("enable for logging", "managed from app.config");

        /// <summary>
        /// c-or
        /// </summary>
        public UserStorageService(IGeneratorId newId, IValidator valid, UserStorageServiceMode mode, IEnumerable<IUserStorageService> services = null)
        {
            users = new List<User>();
            this.mode = mode;
            if (services != null)
            {
                slaveService = services.ToList();
            }
            subscribers = new HashSet<INotificationSubscriber>();
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

            if (IsMaster())
            {
                valid.Validate(user);
                user.Id = newId.Generate();              
                users.Add(user);
                if (slaveService == null) return;
                foreach (var item in slaveService)
                {
                    item.Add(user);
                }
            }
            else
            {
                throw new NotSupportedException();
            }
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

            if (IsMaster())
            {
                valid.Validate(user);
                user.Id = newId.Generate();
                if (slaveService == null) return false;
                foreach (var item in slaveService)
                {
                    item.Remove(user);
                }
                return users.Remove(user);
            }
            else
            {
                throw new NotSupportedException();
            }
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

        public void UserAdded(User user)
        {
            Add(user);
        }

        public void UserRemoved(User user)
        {
            Remove(user);
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));
 
            subscribers.Remove(subscriber);
        }

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            subscribers.Add(subscriber);
        }

        private bool IsMaster()
        {
            var stackTrace = new StackTrace();
            var current= stackTrace.GetFrame(1).GetMethod();
            var frames = stackTrace.GetFrames();
            int flag;
            if (frames == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                flag = frames.Select(x => x.GetMethod()).Count(x => x == current);
            }

            return mode == UserStorageServiceMode.MasterNode || flag >= 2;
        }
    }
}
