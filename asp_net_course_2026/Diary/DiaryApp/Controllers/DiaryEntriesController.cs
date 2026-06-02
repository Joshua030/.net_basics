using DiaryApp.Data;
using Microsoft.AspNetCore.Mvc;
using DiaryApp.Models;

namespace DiaryApp.Controllers
{
    public class DiaryEntriesController : Controller
    {

        private readonly AppDbContext _context;

        public DiaryEntriesController( AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<DiaryEntry> diaryEntries = _context.DiaryEntries.ToList() ?? [];    
            return View(diaryEntries);
        }
    }
}
