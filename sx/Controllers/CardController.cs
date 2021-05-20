using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sx.Models;
using sx.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;

namespace sx.Controllers
{
    public class CardController : Controller
    {
        private AppContext _context;

        public CardController(AppContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Card()
        {
            Dictionary<Goods,char> list = new Dictionary<Goods,char>();
            string s = "";
            foreach (var item in HttpContext.Request.Cookies.Where(x=>x.Value == User.Identity.Name))
            {
                s = item.Key.Substring(0, item.Key.Length - 1);
                list.Add(_context.Goods.Where(x => x.Id.ToString() == s).FirstOrDefault(), item.Key[item.Key.Length-1]);
            }
            return View(list);
        }
        [HttpPost]
        public async Task<ActionResult> AddToCard(string id)
        {
            HttpContext.Response.Cookies.Append(id,User.Identity.Name);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            HttpContext.Response.Cookies.Delete(id);
            return Ok();
        }
    }
}