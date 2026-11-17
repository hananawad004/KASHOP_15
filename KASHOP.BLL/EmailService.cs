using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace KASHOP.BLL
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string to, string subject, string HTMLbody)
        {
         using var client = new SmtpClient(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]))
            {   
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(_configuration["EmailSettings:Email"], _configuration["EmailSettings:password"])
            };
           using var mailMessage =new MailMessage(_configuration["EmailSettings:Email"], to, subject, HTMLbody)
            {
                
                IsBodyHtml = true
            };
           await client.SendMailAsync(mailMessage);
        }
    }
}
