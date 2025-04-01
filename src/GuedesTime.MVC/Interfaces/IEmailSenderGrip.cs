namespace GuedesTime.MVC.Interfaces
{
    public interface IEmailSenderGripGrip
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
