using PasswordManager.Data.Repository;
using PasswordManager.Entities;
using PasswordManager.Services.interfaces;

namespace PasswordManager.Services
{
    public class UserDataInitializer: IUserDataInitializer
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _repository;
        private CancellationTokenSource _cts = new();

        public UserDataInitializer(IUserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public void InitializeData()
        {
            var encryptionKey = _config["EncripyStrings:key"];

            var users = _repository.TableNoTracking.ToList();
            if (users.Count == 0)
            {
                var encryptedPassword = SecurityHelper.Encrypt(encryptionKey, "Admin123!@#");
                var user = new User
                {
                    Name = "administrator",
                    Family = " ",
                    UserName = "admin",
                    UserType = AccountType.Admin,
                    Password = encryptedPassword
                };

                _repository.CreateAsync(user, _cts.Token);
            }
        }
    }
}
