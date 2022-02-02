using MyFlashCards.Domain.Models;

namespace MyFlashCards.Domain.Requests;

public record CreateTestRequest(Guid Id, Prompt Prompt, int NumberOfQuestions, IEnumerable<string> Tags);
