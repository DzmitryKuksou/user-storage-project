using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class Validation : IValidation
    {
        public void Validate(User user)
        {
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
        }

        public void Validate(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("FirstName or lastName is null or empty, or whitespace", nameof(name));
            }
        }

        public void Validate(int age)
        {
            if (age <= 0)
            {
                throw new ArgumentException("Age less than zero", nameof(age));
            }
        }
    }
}
