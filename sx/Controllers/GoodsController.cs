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
using Microsoft.AspNetCore.Mvc.Rendering;


namespace sx.Controllers
{
    public class GoodsController : Controller
    {
        private AppContext _context;

        public GoodsController(AppContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult AllGoods()
        {
            var goods = _context.Goods.Select(x => x);
            return View(goods);
            
        }
        [HttpGet]
        public IActionResult NewGoods()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewGoods(NewGoodsModel model)
        {
            if (ModelState.IsValid)
            {
                Goods good = await _context.Goods.FirstOrDefaultAsync(u => u.ShortName == model.ShortName);
                if (good == null)
                {
                    // добавляем пользователя в бд
                    good = new Goods { 
                        ShortName = model.ShortName, 
                        Description = model.Description, 
                        UrlPhoto = model.UrlPhoto, 
                        DatePublic = System.DateTime.Now, 
                        IdSeller = _context.Users.Where(x=>x.Email == User.Identity.Name).First().Id, 
                        Price = model.Price
                    };
                    _context.Goods.Add(good);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("AllGoods", "Goods");
                }
                else
                    ModelState.AddModelError("", "Некорректное название");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Good(int id)
        {
            var good = _context.Goods.Where(x => x.Id == id).FirstOrDefault();
            return View(good);
        }
        [HttpGet]
        public IActionResult UpdateGoods(int id)
        {
            var good = _context.Goods.Where(x => x.Id == id).FirstOrDefault();
            return View(good);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateGoods(Goods model)
        {
            if (ModelState.IsValid)
            { 
                model.DatePublic = System.DateTime.Now;
                model.IdSeller = _context.Users.Where(x => x.Email == User.Identity.Name).First().Id;
                _context.Goods.Update(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<Goods>> Delete(int id)
        {
            Goods goods = _context.Goods.FirstOrDefault(x => x.Id == id);
            if (goods == null)
            {
                return NotFound();
            }
            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();
            return Ok(goods);
        }

    }
}