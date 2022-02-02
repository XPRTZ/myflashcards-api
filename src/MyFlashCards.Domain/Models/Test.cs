namespace MyFlashCards.Domain.Models;

public record Test(Guid Id, Prompt Prompt, IEnumerable<Question> Questions);
