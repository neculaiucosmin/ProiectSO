using CatalogBackend.Entities;

namespace CatalogBackend.Services;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}