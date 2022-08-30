using PasswordManager.Models;
using PasswordManager.Services.interfaces;

namespace PasswordManager.Services
{
    public class UserStateService
    {
        public UserDto userInfo = new UserDto();

        public UserStateService()
        {
            userInfo = null;
        }

        public async Task Clear()
        {
            userInfo = null;

            await Event?.Invoke();

        }

        public async Task Add(UserDto value)
        {
            userInfo = value;

            await Event?.Invoke();

        }

        public event Func<Task> Event;
    }
}
