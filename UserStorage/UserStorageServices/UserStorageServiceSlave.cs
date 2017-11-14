using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageServiceSlave : UserStorageServiceBase, INotificationSubscriber
    {
        public UserStorageServiceSlave(IGeneratorId generator, IValidator validator) : base(generator, validator)
        {
        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.SlaveNode;

        public override void Add(User user)
        {
            if (IsMaster())
            {
                base.Add(user);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public override bool Remove(User user)
        {
            if (this.IsMaster())
            {
                return base.Remove(user);
            }

            throw new NotSupportedException();
        }

        public override void UserAdded(User user)
        {
            Add(user);
        }

        public override void UserRemoved(User user)
        {
            Remove(user);
        }

        private bool IsMaster()
        {
            var stackTrace = new StackTrace();
            var current = stackTrace.GetFrame(1).GetMethod();
            var method = typeof(UserStorageServiceMaster).GetMethod(current.Name);
            var frames = stackTrace.GetFrames();
            bool flag;
            if (frames == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                flag = frames.Select(x => x.GetMethod()).Contains(method);
            }

            return flag;
        }
    }
}
