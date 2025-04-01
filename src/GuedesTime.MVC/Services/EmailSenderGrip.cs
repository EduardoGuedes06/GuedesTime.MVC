using GuedesTime.MVC.Interfaces;
using System.Net.Mail;

namespace GuedesTime.MVC.Services
{
    public class EmailSenderGrip : IEmailSenderGripGrip
    {
        private readonly string _sendGridApiKey;

        public EmailSenderGrip(IConfiguration configuration)
        {
            _sendGridApiKey = configuration.GetValue<string>("SendGrid:ApiKey");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Lógica de envio de e-mail com o SendGrid usando _sendGridApiKey
        }
    }

}
