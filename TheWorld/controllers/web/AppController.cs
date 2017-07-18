using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.viewModels;
using TheWorld.Services;
using TheWorld.models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace TheWorld.controllers.web
{
    public class AppController:Controller
    {
        private IMailService mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;
        public AppController(IMailService mailService,
            IConfigurationRoot _config,
            IWorldRepository _repository,
            ILogger<AppController> _logger)
        {
            this.mailService = mailService;
            this._config = _config;
            this._repository = _repository;
            this._logger = _logger;
        }
        public IActionResult index()
        {
            return View();   
        }

        [Authorize]
        public IActionResult trips()
        {
            //try
            //{
            //    var data = _repository.getAllTrips();
            //    return View(data);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError($"Failed to get all trips in index page:{ex.Message}");
            //    return Redirect("/error");
            //}
            return View();
        }

        public IActionResult contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult contact(ContactViewModel model)
        {
            if (model.email.Contains("aol.com"))
            {
                ModelState.AddModelError("","We don't support aol address");
            }
            if (ModelState.IsValid)
            {
                mailService.sendMail(_config["MailSettings:ToAddress"], model.email, "From world website", model.message);
                ModelState.Clear();
                ViewBag.SentMessage = "Message sent";
            }
            return View();
        }

        public IActionResult about()
        {
            return View();
        }
    }
}
