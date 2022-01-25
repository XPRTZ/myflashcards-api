namespace MyFlashCards.Domain.Models;

public record Card(Guid Id, string Front, string Back, string[] Tags);
