using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.BLL
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string HTMLbody);
    }
}
