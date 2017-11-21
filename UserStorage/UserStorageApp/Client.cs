using System.Configuration;
using UserStorageServices;

namespace UserStorageApp
{
    /// <summary>
    /// Represents a client that uses an instance of the <see cref="UserStorageServiceBase"/>.
    /// </summary>
    public class Client
    {
        private IUserStorageService _userStorageService;
        private IUserRepository repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client(IUserStorageService userStorageService = null, IUserRepository repository = null)
        {
            var filePath = ConfigurationManager.AppSettings["FilePath"];
            this.repository = repository ?? new UserMemoryCacheWithState(filePath);
            _userStorageService = userStorageService ?? new UserStorageServiceLog(new UserStorageServiceMaster(repository));
        }

        /// <summary>
        /// Runs a sequence of actions on an instance of the <see cref="UserStorageServiceBase"/> class.
        /// </summary>
        public void Run()
        {


            repository.Start();

            _userStorageService.Add(new User
            {
                FirstName = "Alex",
                LastName = "Black",
                Age = 25
            });
            repository.Stop();
        }
    }
}
