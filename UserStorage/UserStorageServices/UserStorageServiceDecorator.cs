using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public abstract class UserStorageServiceDecorator : IUserStorageService
    {
        protected readonly IUserStorageService storageService;

        protected UserStorageServiceDecorator(IUserStorageService storageService)
        {
            this.storageService = storageService;
        }

        public abstract int Count { get; }

        public abstract UserStorageServiceMode ServiceMode { get; }

        public abstract void Add(User user);

        public abstract bool Remove(User user);

        public abstract IEnumerable<User> SearchByFirstName(string firstName);

        public abstract IEnumerable<User> SearchByLastName(string lastName);

        public abstract IEnumerable<User> SearchByAge(int age);

        public abstract IEnumerable<User> SearchByPredicate(Predicate<User> predicate);
    }
}
