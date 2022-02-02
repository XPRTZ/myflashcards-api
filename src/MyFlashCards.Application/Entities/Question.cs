using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFlashCards.Application.Entities;

public class Question
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Card Card { get; set; } = default!;
    
    public bool? Correct { get; set; }
}
