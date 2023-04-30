using Microsoft.AspNetCore.Mvc;
using OrarBackend.Entities;
using OrarBackend.Services;

namespace OrarBackend.Controllers;

[Route("api/")]
public class MailController : Controller
{
    private readonly IMailService _mailService;

    public MailController(IMailService service)
    {
        _mailService = service;
    }

    [HttpPost("send_mail")]
    public async Task<ActionResult<MailRequest>> SendMail(MailRequest request)
    {
        Console.WriteLine(request.ToString());
        await _mailService.SendEmailAsync(request);
        return Ok();
    }
}