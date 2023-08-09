namespace StoreMeDaddy.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private static readonly string SpecialChars = @"%!#$%^&*()?/><,|}]{~`+=";
        public UserModel(string username, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (username.Length < 3 || username.Any(u => SpecialChars.Contains(u)))
            {
                throw new ArgumentException("Username must be at least 3 characters long.");
            }
            if (password.Length < 3 || !password.Any(u => SpecialChars.Contains(u)))
            {
                throw new ArgumentException("Password must be at least 3 characters long. Password must contain at least one special character");
            }
            Username = username;
            Password = password;
        }
    }
}