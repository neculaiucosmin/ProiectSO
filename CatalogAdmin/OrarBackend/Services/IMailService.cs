using OrarBackend.Entities;

namespace OrarBackend.Services;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}