using DutchTreat.Data;
using DutchTreat.Model;
using DutchTreat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController: Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;
        private readonly DutchContext _context;

        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
            //_context = context;
        }
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            //ViewBag.title = "Contact Us";
            //throw new InvalidOperationException("Bad things happen");
            return View();
        }


        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            //ViewBag.title = "Contact Us";
            //throw new InvalidOperationException("Bad things happen");
            if (ModelState.IsValid)
            {
                // send the mail
                _mailService.SendMail("abc@example.com", model.Subject, $"From : {model.Name} - Email: {model.Email} - Message: {model.Message}" );
                ViewBag.UserMessage = "Message Sent";
                ModelState.Clear();
            }
            else
            { 
                // show errors
            }
            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            ViewBag.title = "About Us";
            return View();
        }

        [Authorize]
        [Route("shop")]
        public IActionResult Shop()
        {
            ViewBag.title = "Shop";
            //var results = _context.Products.ToList();
           // var results = _repository.GetAllProducts();
            return View();
        }
    }
}
