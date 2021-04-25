using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.SendingService
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // add your code to send email
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // add your code to send sms
            return Task.FromResult(0);
        }
    }
}
