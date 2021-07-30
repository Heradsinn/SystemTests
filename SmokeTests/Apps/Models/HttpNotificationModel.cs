namespace SmokeTests.Apps.Models
{
    public class HttpNotificationModel : NotificationModel
    {
        public string ServerAddress { get; set; }
        public string TransactionType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
