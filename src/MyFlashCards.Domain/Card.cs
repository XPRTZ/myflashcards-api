namespace MyFlashCards.Domain;

public record Card(Guid id, string Front, string Back, string[] Tags);
