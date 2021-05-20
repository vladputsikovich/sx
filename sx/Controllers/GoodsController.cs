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
        public IActionResult AllGoods(string type)
        {
            var goods = _context.Goods.Include(x => x.Size);
            if (type != null)
            {
                goods = _context.Goods.Where(x=>x.Category.Type.ToString() == type).Include(x => x.Size);
                return Ok(goods);
            }
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
                List<Size> sizes = new List<Size>();
                foreach (var item in model.Size)
                {
                    sizes.Add(item);
                    _context.Sizes.Add(item);
                }
                Goods good = await _context.Goods.FirstOrDefaultAsync(u => u.ShortName == model.ShortName);
                if (good == null)
                {
                    good = new Goods {
                        ShortName = model.ShortName,
                        Description = model.Description,
                        UrlPhoto = model.UrlPhoto,
                        DatePublic = System.DateTime.Now,
                        IdSeller = _context.Users.Where(x => x.Email == User.Identity.Name).First().Id,
                        Price = model.Price,
                        Size = sizes,
                        Category = new Category { Type = model.Category.Type, Kind = model.Category.Kind}
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
            var good = _context.Goods.Include(x => x.Size).Where(x => x.Id == id).FirstOrDefault();
            return View(good);
        }
        [HttpGet]
        public IActionResult UpdateGoods(int id,string result)
        {
            var good = _context.Goods.Where(x => x.Id == id).FirstOrDefault();
            if (result == "ok")
            {
                ViewBag.Message = "Goods update success";
            }
            if(result == "bad")
            {
                ViewBag.Message = "Goods update with proplem";
            }
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
                return RedirectToAction("UpdateGoods","Goods",new { id = model.Id, result = "ok" });
            }
            return RedirectToAction("UpdateGoods", "Goods", new { id = model.Id, result = "bad" });
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