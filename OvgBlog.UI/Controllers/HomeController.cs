using MailKit.Net.Smtp;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using OvgBlog.Business.Abstract;
using OvgBlog.DAL.Data;
using OvgBlog.UI.Extentions;
using OvgBlog.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OvgBlog.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IContactService _contactService;

        public HomeController(ILogger<HomeController> logger, 
            IArticleService articleService, 
            ICategoryService categoryService,
            IConfiguration configuration,
            IContactService contactService
            )
        {
            _configuration = configuration;
            _logger = logger;
            _articleService = articleService;
            _categoryService = categoryService;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {          
            var model = new HomeViewModel();

           

            return View(model);
        }

        public IActionResult Categories(CancellationToken cancellationToken)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(model);
        }
        [HttpGet]
        public IActionResult AboutMe(CancellationToken cancellationToken)
        {

            return View();
        }
        //[HttpPost]
        //public IActionResult SendEmail(SendEmailViewModel sendEmailViewModel)
        //{           
        //    if (ModelState.IsValid)
        //    {
        //        if (!sendEmailViewModel.Email.EmailValidation())
        //        {
        //            return Json(new JsonResultModel<SendEmailViewModel>(false, "Lütfen doğru bir mail adresi giriniz."));
        //        }
        //        var message = new MimeMessage();
        //        message.From.Add(new MailboxAddress(sendEmailViewModel.Name, sendEmailViewModel.Email));
        //        message.To.Add(new MailboxAddress("Orhan", _configuration["GmailReceiverMail"].ToString()));
        //        message.Subject = sendEmailViewModel.Name;
        //        message.Body = new TextPart("plain")
        //        {
        //            Text = "\nEmail:\n" + sendEmailViewModel.Email+ "\nAdı soyadı:\n" + sendEmailViewModel.Name + "\nMesajı:\n" + sendEmailViewModel.Body
        //        };
        //        using (var client = new SmtpClient())
        //        {
        //            client.Connect("smtp.gmail.com", 587, false);                  
        //            client.Authenticate(_configuration["GmailSenderMail"].ToString(), _configuration["GmailSenderPassword"].ToString());
        //            client.Send(message);
        //            client.Disconnect(true);
        //        }
        //        return Json(new JsonResultModel<SendEmailViewModel>(true, "Gönderildi."));
        //    }
        //    else
        //    {
        //        return Json(new JsonResultModel<SendEmailViewModel>(false, "Eksik alanları doldurun."));
        //    }

        //}
        [HttpPost]
        public async Task<IActionResult> AddContact(ContactListViewModel contactListViewModel, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonResultModel<ContactListViewModel>(false, "Alanları doldurun."));
            }
            var model = contactListViewModel.Adapt<Contact>();
            await _contactService.CreateAsync(model, cancellationToken);
            return Json(new JsonResultModel<ContactListViewModel>(true, "Mesajınız iletildi en kısa sürede geri dönüş yapılacaktır."));
        }
    }
}
