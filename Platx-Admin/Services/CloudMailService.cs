namespace Platx_Admin.Services
{
    public class CloudMailService : IMailServices
    {
        private string _mailTo = string.Empty;
        private string _mailFrom = string.Empty;

        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailsettings:mailToAddress"];
            _mailFrom = configuration["mailsettings:mailFromAddress"];
        }

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with {nameof(CloudMailService)}");
            Console.WriteLine($"Subject {subject}");
            Console.WriteLine($"Message {message}");

        }
    }


}

