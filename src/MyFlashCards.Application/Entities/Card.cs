using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFlashCards.Application.Entities;

public class Card
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [StringLength(50)]
    public string Front { get; set; } = default!;
    
    [StringLength(500)]
    public string Back { get; set; } = default!;
    
    public IEnumerable<Tag> Tags { get; set; } = Enumerable.Empty<Tag>();
}
