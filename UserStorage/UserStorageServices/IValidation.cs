using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public interface IValidation
    {
        void Validate(User user);

        void Validate(int age);

        void Validate(string name);
    }
}
