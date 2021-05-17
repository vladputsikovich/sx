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
            List<Goods> list = new List<Goods>();
            foreach (var item in HttpContext.Request.Cookies.Where(x=>x.Value == User.Identity.Name))
            {
                list.Add(_context.Goods.Where(x => x.Id.ToString() == item.Key).FirstOrDefault());
            }
            return View(list);
        }
        [HttpPost]
        public async Task<ActionResult> AddToCard(int id)
        {
            HttpContext.Response.Cookies.Append($"{id}",User.Identity.Name);
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            HttpContext.Response.Cookies.Delete($"{id}");
            return Ok();
        }
    }
}