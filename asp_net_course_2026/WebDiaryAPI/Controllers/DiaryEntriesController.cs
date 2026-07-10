using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebDiaryAPI.Data;
using WebDiaryAPI.Models;

namespace WebDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController(ApplicationDbContext context) : ControllerBase
    {

        [HttpGet]
        public IEnumerable<DiaryEntry> GetDiaryEntries()
        {
            //return context.DiaryEntries.ToList();
            return [.. context.DiaryEntries];
        }

    }
}
