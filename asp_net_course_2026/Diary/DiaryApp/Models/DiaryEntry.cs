

using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Models;

public class DiaryEntry
{
    //[key]
    public int Id { get; set; }
    [Required]
    public required string Title { get; set; }
    //NOTE - Other variables to declare required values withou errors
    /*    public required string? Title { get; set; }
       public required string? Title { get; set; } = ""; */


    public string? Content { get; set; }
    [Required]
    public DateTime Created { get; set; } = DateTime.Now;
}


