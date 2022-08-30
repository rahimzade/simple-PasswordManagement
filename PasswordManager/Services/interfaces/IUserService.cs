using PasswordManager.Entities;

namespace PasswordManager.Services.interfaces
{
    public interface IUserService
    {
        Task<User> Add(User user, CancellationToken cancellationToken);
        Task<bool> Update(int id, User user, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
        Task<List<User>> GetAll(CancellationToken cancellationToken);
        Task<User> GetById(int id, CancellationToken cancellationToken);
        Task<User> Signin(string username, string password, CancellationToken cancellationToken);
    }
}
