using Arna_Project_Track.Models;
using Arna_Project_Track.services;
using Microsoft.AspNetCore.Mvc;

namespace Arna_Project_Track.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmail _email;

        public EmailController(IEmail email) {
            _email = email;
        }
        [HttpGet]
        public IActionResult EmailSend()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailSend(EmailModel emailModel)
        {
            if (ModelState.IsValid)
            {
                await _email.SendEmailAsync(emailModel);
                ViewBag.Message = "Email sent successfully!";
            }

            return View(emailModel);
        }

    }
}
