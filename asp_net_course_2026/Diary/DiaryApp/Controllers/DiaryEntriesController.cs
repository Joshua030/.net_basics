using DiaryApp.Data;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.Models;

namespace DiaryApp.Controllers
{
    public class DiaryEntriesController : Controller
    {

        private readonly AppDbContext _context;

        public DiaryEntriesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<DiaryEntry> diaryEntries = _context.DiaryEntries.ToList() ?? [];
            return View(diaryEntries);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DiaryEntry diaryEntry)
        {
            if (!ModelState.IsValid)
            {
                return View(diaryEntry);
            }

            _context.DiaryEntries.Add(diaryEntry);
            _context.SaveChanges();
            return RedirectToAction("Index", "DiaryEntries");
        }
    }
}
