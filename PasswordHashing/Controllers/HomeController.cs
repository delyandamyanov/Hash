using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PasswordHashing.Models;

namespace PasswordHashing.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(Hasher hasher)
        {
            return View(hasher);
        }
        

        [HttpPost]
        public IActionResult GetHash(Hasher hasher)
        {
            try
            {
                hasher.GetHashedPassword();
            }
            catch (ArgumentNullException)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", hasher);
        }
    }
}
