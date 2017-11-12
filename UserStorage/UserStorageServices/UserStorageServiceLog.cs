using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UserStorageServices
{
    public class UserStorageServiceLog : UserStorageServiceDecorator
    {
        public UserStorageServiceLog(IUserStorageService storageService) : base(storageService)
        {
        }

        private readonly BooleanSwitch logging = new BooleanSwitch("Enable logging", "managed from app.config");

        public override int Count
        {
            get
            {
                if (logging.Enabled)
                {
                    Trace.TraceInformation("Count() method is called.");
                }

                return storageService.Count;
            }
        }

        public override UserStorageServiceMode ServiceMode => ServiceMode;

        public override void Add(User user)
        {
            if (logging.Enabled)
            {
                Trace.TraceInformation("Add() method is called.");
            }

            storageService.Add(user);
        }

        public override bool Remove(User user)
        {
            if (logging.Enabled)
            {
                Trace.TraceInformation("Remove() method is called.");
            }

            return storageService.Remove(user);
        }

        public override IEnumerable<User> SearchByFirstName(string firstName)
        {
            if (logging.Enabled)
            {
                Trace.TraceInformation("SearchByFirstName() method is called.");
            }

            return storageService.SearchByFirstName(firstName);
        }

        public override IEnumerable<User> SearchByLastName(string lastName)
        {
            if (logging.Enabled)
            {
                Trace.TraceInformation("SearchByLastName() method is called.");
            }

            return storageService.SearchByLastName(lastName);
        }

        public override IEnumerable<User> SearchByAge(int age)
        {
            if (logging.Enabled)
            {
                Trace.TraceInformation("SearchByAge() method is called.");
            }

            return storageService.SearchByAge(age);
        }

        public override IEnumerable<User> SearchByPredicate(Predicate<User> predicate)
        {
            if (logging.Enabled)
            {
                Trace.TraceInformation("SearchByPredicate() method is called.");
            }

            return storageService.SearchByPredicate(predicate);
        }
    }
}
