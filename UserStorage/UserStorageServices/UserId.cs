using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserId : IGeneratorId
    {
        /// <summary>
        /// generate new id
        /// </summary>
        /// <returns></returns>
        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
