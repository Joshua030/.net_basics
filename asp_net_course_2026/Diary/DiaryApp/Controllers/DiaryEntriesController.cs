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

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            DiaryEntry? diaryEntry = _context.DiaryEntries.Find(id);
            if (diaryEntry == null) return NotFound();
            return View(diaryEntry);
        }
    }
}
