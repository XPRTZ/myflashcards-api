using System.ComponentModel.DataAnnotations;

namespace MyFlashCards.Application.Entities;

public class Question
{
    [Key]
    public Guid Id { get; set; }

    public Test Test { get; set; } = default!;

    public Card Card { get; set; } = default!;
    
    public bool? Correct { get; set; }
}
