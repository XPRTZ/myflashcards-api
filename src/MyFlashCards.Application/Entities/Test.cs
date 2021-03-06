using MyFlashCards.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFlashCards.Application.Entities;

public class Test
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    public Prompt Prompt { get; set; }

    public ICollection<Question> Questions { get; set; } = default!;
}
