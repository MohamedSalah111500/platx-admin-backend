namespace Platx_Admin.Services
{
    public interface IMailServices
    {
        void Send(string subject, string message);
    }
}