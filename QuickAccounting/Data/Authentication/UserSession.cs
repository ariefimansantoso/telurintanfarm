namespace QuickAccounting.Data.Authentication
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
