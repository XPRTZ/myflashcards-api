using System.ComponentModel.DataAnnotations;

namespace MyFlashCards.Application.Entities;

public class Tag
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Value { get; set; } = default!;
    
    public IEnumerable<Card> Cards { get; set; } = Enumerable.Empty<Card>();
}
