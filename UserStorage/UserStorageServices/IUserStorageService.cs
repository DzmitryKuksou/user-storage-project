using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IUserStorageService
    {
        int Count { get; }

        void Add(User user);

        bool Remove(User user);

        IEnumerable<User> SearchByAge(int age);

        IEnumerable<User> SearchByLastName(string lastName);

        IEnumerable<User> SearchByFirstName(string firstName);

        IEnumerable<User> SearchByPredicate(Predicate<User> predicate);
    }
}
