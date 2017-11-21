﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageServiceMaster : UserStorageServiceBase
    {
        private readonly IEnumerable<IUserStorageService> slaveService;

        private readonly HashSet<INotificationSubscriber> subscribers;

        private event Action<User> AddedToStorage;

        private event Action<User> RemovedFromStorage;
        private readonly IValidator valid;


        public UserStorageServiceMaster(IUserRepository repository, IValidator valid = null, IEnumerable<IUserStorageService> services = null) : base(repository)
        {
            this.valid = valid??new CompositeValidator();
            this.slaveService = slaveService?.ToList() ?? new List<IUserStorageService>();
            subscribers = new HashSet<INotificationSubscriber>();
        }

        public override UserStorageServiceMode ServiceMode => UserStorageServiceMode.MasterNode;

        public override void Add(User user)
        {
            valid.Validate(user);

            base.Add(user);

            OnUserAdded(user);

            foreach (var item in slaveService)
            {
                item.Add(user);
            }
        }

        public override bool Remove(User user)
        {
            valid.Validate(user);
            foreach (var item in subscribers)
            {
                item.UserRemoved(user);
            }

            OnUserRemoved(user);

            return base.Remove(user);
        }

        public void AddSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(INotificationSubscriber subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

            subscribers.Remove(subscriber);
        }

        private void OnUserAdded(User user)
        {
            AddedToStorage?.Invoke(user);
        }

        private void OnUserRemoved(User user)
        {
            RemovedFromStorage?.Invoke(user);
        }
    }
}
