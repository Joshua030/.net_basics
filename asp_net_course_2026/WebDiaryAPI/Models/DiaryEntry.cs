using System.ComponentModel.DataAnnotations;

namespace WebDiaryAPI.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
