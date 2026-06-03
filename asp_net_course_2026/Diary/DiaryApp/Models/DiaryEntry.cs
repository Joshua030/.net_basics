

using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Models;

public class DiaryEntry
{
    //[key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter a title for your diary entry.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "The title must be between 3 and 100 characters.")]
    public required string Title { get; set; }
    //NOTE - Other variables to declare required values withou errors
    /*    public required string? Title { get; set; }
       public required string? Title { get; set; } = ""; */

    [Required(ErrorMessage = "Please enter content for your diary entry.")]
    public required string Content { get; set; }
    [Required(ErrorMessage = "Please enter a date for your diary entry.")]
    public DateTime Created { get; set; } = DateTime.Now;
}


