using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices.Validation
{
    public class AgeValidator : IValidator
    {
        public void Validate(User user)
        {
            if (user.Age <= 0)
            {
                throw new ArgumentException("Age less than zero.", nameof(user.Age));
            }
        }
    }
}
