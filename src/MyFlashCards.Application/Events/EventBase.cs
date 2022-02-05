using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFlashCards.Application.Events;

public abstract class EventBase
{
    [Key] 
    public Guid Id { get; set; }
    public Guid StreamId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StreamPosition { get; set; }

    public DateTime Timestamp { get; set; }

    [Column("Data")] 
    public string DataString { get; set; } = default!;
}
