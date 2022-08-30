using Microsoft.EntityFrameworkCore;
using PasswordManager.Data;
using PasswordManager.Entities;

namespace PasswordManager.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public virtual IQueryable<User> Table => _dbContext.Set<User>();
        public virtual IQueryable<User> TableNoTracking => _dbContext.Set<User>().AsNoTracking();

        public async Task<User> SigninAsync(string username, string password, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                var obj = await _dbContext.Users.AddAsync(user);
                _dbContext.SaveChanges();
                return obj.Entity;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var data = _dbContext.Users.FirstOrDefault(x => x.Id == id);
                if (data != null)
                {
                    _dbContext.Remove(data);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int Id, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }
    }
}
