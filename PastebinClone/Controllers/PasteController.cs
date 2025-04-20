using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PastebinClone.Data;
using PastebinClone.Models;
using System.Threading.Tasks;

namespace PastebinClone.Controllers
{
    public class PasteController : Controller
    {
        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;

        public PasteController(ApplicationContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(string text)
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            Paste paste = new Paste(text, user.Id);
            context.Pastes.Add(paste);
            await context.SaveChangesAsync();
            return RedirectToAction("Content", new { urlId = paste.UrlID });
        }

        [HttpGet("{urlId}")]
        public IActionResult Content(string urlId)
        {
            var paste = context.Pastes.FirstOrDefault(p => p.UrlID == urlId);
            if (paste == null)
            {
                return View("NotFound");
            }
            return View(paste);
        }

        [Authorize]
        public async Task<IActionResult> UserPastes()
        {
            var user = await userManager.GetUserAsync(User);

            if(user == null)
            {
                return Unauthorized();
            }

            var pastes = context.Pastes.Where(p => p.UserId == user.Id).ToList();

            return View(pastes);
        }

        [HttpPost("extend/{urlId}")]
        public async Task<IActionResult> ExtendPaste(string urlId)
        {
            var paste = context.Pastes.Where(p => p.UrlID == urlId).FirstOrDefault();

            if(paste == null)
            {
                return NotFound();
            }

            paste.ExtendExpirationDate();
            await context.SaveChangesAsync();

            return RedirectToAction("UserPastes");
        }

        [HttpPost("delete/{urlId}")]
        public async Task<IActionResult> DeletePaste(string urlId)
        {
            var paste = context.Pastes.Where(p => p.UrlID == urlId).FirstOrDefault();

            if (paste == null)
            {
                return NotFound();
            }

            context.Remove(paste);
            await context.SaveChangesAsync();

            return RedirectToAction("UserPastes");
        }
    }
}
