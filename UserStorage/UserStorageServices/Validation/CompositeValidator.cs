using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserStorageServices.Validation;

namespace UserStorageServices
{
    public class CompositeValidator : IValidator
    {

        private readonly IValidator[] _validators;

        public CompositeValidator()
        {
            _validators = new[]
             {
                 new AgeValidator(),
                 new FirstNameValidator(),
                (IValidator)new LastNameValidator()
             };
        }

        public void Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User is null." , nameof(user));
            }

            foreach (var item in _validators)
            {
                item.Validate(user);
            }
        }
    }
}
