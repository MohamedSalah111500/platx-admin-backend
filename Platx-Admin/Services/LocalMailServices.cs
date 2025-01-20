namespace Platx_Admin.Services
{
    public class LocalMailServices : IMailServices
    {
        private string _mailTo = string.Empty;
        private string _mailFrom = string.Empty;

        public LocalMailServices(IConfiguration configuration)
        {
            _mailTo = configuration["mailsettings:mailToAddress"];
            _mailFrom = configuration["mailsettings:mailFromAddress"];
        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(LocalMailServices)}");
            Console.WriteLine($"Subject {subject}");
            Console.WriteLine($"Message {message}");

        }
    }


}
