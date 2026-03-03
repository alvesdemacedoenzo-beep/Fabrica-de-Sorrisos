namespace FabricaDeSorrisos.UI.Models
{
    public static class UserSession
    {
        public static string Token { get; set; }
        public static string UserName { get; set; }
        public static string Role { get; set; }

        public static bool IsAuthenticated =>
            !string.IsNullOrWhiteSpace(Token);

        public static void Logout()
        {
            Token = null;
            UserName = null;
            Role = null;
        }
    }
}
