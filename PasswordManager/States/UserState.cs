using PasswordManager.Models;

namespace PasswordManager.States
{
    public class UserState
    {
        // _currentInfo holds the current user value
        // for the entire application
        private UserDto? _currentInfo;
        // StateChanged is an event handler other pages
        // can subscribe to 
        public event EventHandler StateChanged;
        // This method will always return the current user
        public UserDto GetCurrentInfo()
        {
            return _currentInfo;
        }
        // This method will be called to update the current user
        public void SetCurrentInfo(UserDto param)
        {
            _currentInfo = param;
            StateHasChanged();
        }
        // This method will allow us to reset the current user
        public void ResetCurrentInfo()
        {
            _currentInfo = null;
            StateHasChanged();
        }
        private void StateHasChanged()
        {
            // This will update any subscribers
            // that the user state has changed
            // so they can update themselves
            // and show the current user value
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
