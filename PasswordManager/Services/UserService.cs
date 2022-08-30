using PasswordManager.Entities;
using System.Collections.Generic;
using System;
using PasswordManager.Data.Repository;
using PasswordManager.Services.interfaces;
using System.Linq;

namespace PasswordManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<User> Signin(string username, string password, CancellationToken cancellationToken)
        {
            try
            {
                var encryptionKey = _config["EncripyStrings:key"];
                var encryptedPassword = SecurityHelper.Encrypt(encryptionKey, password);

                var user = await _userRepository.SigninAsync(username, encryptedPassword, cancellationToken);
                if (user != null)
                    user.Password = SecurityHelper.Decrypt(encryptionKey, user.Password);
                return user;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<User> Add(User user, CancellationToken cancellationToken)
        {
            try
            {
                var encryptionKey = _config["EncripyStrings:key"];
                user.Password = SecurityHelper.Encrypt(encryptionKey, user.Password);

                return await _userRepository.CreateAsync(user, cancellationToken);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _userRepository.DeleteAsync(id, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<List<User>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return await _userRepository.GetAllUsersAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<User> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var encryptionKey = _config["EncripyStrings:key"];

                var user = await _userRepository.GetUserByIdAsync(id, cancellationToken);
                if (user != null)
                    user.Password = SecurityHelper.Decrypt(encryptionKey, user.Password);

                return user;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<bool> Update(int id, User user, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _userRepository.GetUserByIdAsync(id, cancellationToken);
                if (data != null)
                {
                    var encryptionKey = _config["EncripyStrings:key"];

                    data.UserName = user.UserName;
                    data.Password = SecurityHelper.Encrypt(encryptionKey, user.Password);

                    data.Name = user.Name;
                    data.Family = user.Family;
                    data.UserType = user.UserType;

                    await _userRepository.UpdateAsync(data, cancellationToken);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.Message);
                throw;
            }
        }
    }
}
