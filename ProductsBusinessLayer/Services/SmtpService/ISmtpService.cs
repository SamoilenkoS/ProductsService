using ProductsBusinessLayer.DTOs;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Services.SmtpService
{
    public interface ISmtpService
    {
        Task SendMailAsync(MailDTO mailDTO);
    }
}