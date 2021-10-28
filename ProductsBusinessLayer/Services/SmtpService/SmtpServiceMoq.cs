using ProductsBusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ProductsBusinessLayer.Services.SmtpService
{
    public class SmtpServiceMoq : ISmtpService
    {
        public async Task SendMailAsync(MailDTO mailDTO)
        {
            using (var streamWritter = new StreamWriter("log.txt"))
            {
                await streamWritter.WriteLineAsync(mailDTO.Body);
            }
        }
    }
}
