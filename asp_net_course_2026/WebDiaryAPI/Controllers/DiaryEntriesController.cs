using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDiaryAPI.Data;
using WebDiaryAPI.Models;

namespace WebDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController(ApplicationDbContext context) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryEntry>>> GetDiaryEntries()
        {
            //return context.DiaryEntries.ToList();
            return await context.DiaryEntries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntry>> GetDiaryEntryById(int id)
        {

            var diaryEntry = await context.DiaryEntries.FindAsync(id);

            if (diaryEntry == null) return NotFound();

            return diaryEntry;
        }

        [HttpPost]

        public async Task<ActionResult<DiaryEntry>> CreateDiaryEntry(DiaryEntry diaryEntry)
        {
            diaryEntry.Id = 0;

            context.DiaryEntries.Add(diaryEntry);
            await context.SaveChangesAsync();

            var resourceUrl = Url.Action(nameof(GetDiaryEntryById), new { id = diaryEntry.Id });
            return Created(resourceUrl, diaryEntry);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateDiaryEntryById(int id, [FromBody] DiaryEntry diaryEntry)
        {
            if (id != diaryEntry.Id)
            {
                return BadRequest();
            }

            context.Entry(diaryEntry).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                if (!DiaryEntryExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaryEntryById(int id)
        {
            var diaryEntry = await context.DiaryEntries.FindAsync(id);
            if (diaryEntry == null) return NotFound();

            context.DiaryEntries.Remove(diaryEntry);
            await context.SaveChangesAsync();

            return NoContent();

        }

        private bool DiaryEntryExist(int id)
        {
            return context.DiaryEntries.Any(d => d.Id == id);
        }




    }
}
