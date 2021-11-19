using System.Threading.Tasks;
using OngProject.Application.DTOs.Mails;

namespace OngProject.Application.Interfaces.Mail
{
    public interface IMailService
    {
        Task SendMail(SendMailDto sendMailDto);
    }
}