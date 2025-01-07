using ProjectMVC.DAL.Models;

namespace ProjectMVC.PL.Helpers
{
    public interface IMailSettings
    {
        public void SendEmail(Email email);
    }
}
