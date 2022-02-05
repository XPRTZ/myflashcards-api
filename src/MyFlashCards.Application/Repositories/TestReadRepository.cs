using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Repositories;

internal class TestReadRepository : ITestReadRepository
{
    private readonly IFlashCardsContext _context;

    public TestReadRepository(IFlashCardsContext context) => _context = context;

    public Test Get(Guid id)
    {
        var test = 
            _context.Tests
                .Include(x => x.Questions)
                .ThenInclude(x => x.Card)
                .SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Test with id {id} could not be found");

        return GetTest(test);
    }

    private static Test GetTest(Entities.Test test) => new (test.Id, test.Prompt, GetQuestions(test));

    private static IEnumerable<Question> GetQuestions(Entities.Test test) =>
        test.Questions.Select(q => new Question(GetCard(q.Card), q.Correct));

    private static Card GetCard(Entities.Card card) => new (card.Id, card.Front, card.Back, card.Tags);
}
