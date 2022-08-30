using PasswordManager.Entities;

namespace PasswordManager.Data.Repository
{
    public interface IUserRepository
    {
        IQueryable<User> Table { get; }
        IQueryable<User> TableNoTracking { get; }

        public Task<User> CreateAsync(User user, CancellationToken cancellationToken);
        public Task UpdateAsync(User user, CancellationToken cancellationToken);
        public Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);
        public Task<User> GetUserByIdAsync(int Id, CancellationToken cancellationToken);
        public Task DeleteAsync(int id, CancellationToken cancellationToken);
        public Task<User> SigninAsync(string username,string password, CancellationToken cancellationToken);
    }
}
