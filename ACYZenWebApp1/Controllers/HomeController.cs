﻿using System;
using System.Linq;
using ACYZenWebApp1.Controllers.BLZenAutomation;
using Microsoft.AspNetCore.Mvc;
using ACYZenWebApp1.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ACYZenWebApp1.Controllers
{
    public class HomeController : Controller
    {
        DZenActionContext db;
        public HomeController(DZenActionContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.ZenActions.ToList());
        }
        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.ZenActionID = id;
            return View();
        }
        [HttpPost]
        public async Task<string> Buy(Channel channel)
        {
            channel._chanelUrl = await MainOfBLZenAutomation.Registration3NewAccount1Number(channel._firstName, channel._surname, channel._login, channel._channnelName,channel._loginNuberPre, channel._loginNuberPost);
            
            db.Channels.Add(channel);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, " + channel._firstName + ", за покупку!";
        }
    }
}



/*using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ACYZenWebApp1.Models;

namespace ACYZenWebApp1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}*/