namespace WeAreCars.Services
{
    public static class SessionService
    {
        public static bool IsAuthenticated { get; private set; }

        public static string Username { get; private set; } = string.Empty;


        public static void Login(string username)
        {
            IsAuthenticated = true;

            Username = username;
        }


        public static void Logout()
        {
            IsAuthenticated = false;

            Username = string.Empty;
        }
    }
}