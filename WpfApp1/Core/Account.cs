namespace WpfApp1.Core
{
    public class Account
    {
        public string Username { get; private set; }
        public string Email { get; private set; }
        public bool IsAdmin { get; private set; }


        public Account(string username,string email, string elev)
        {
            Username = username;
            Email = email;
            IsAdmin = elev.Equals("all") ? true : false;
        }


        public Account(string username)
        {
            Username = username;
        }
    }
}
