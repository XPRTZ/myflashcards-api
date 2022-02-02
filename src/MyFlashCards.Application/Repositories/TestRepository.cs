using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Extensions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;
using MyFlashCards.Domain.Requests;

namespace MyFlashCards.Application.Repositories;

internal class TestRepository : ITestRepository
{
    private readonly IFlashCardsContext _context;

    public TestRepository(IFlashCardsContext context) => _context = context;

    public Task<Test> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var test = 
            _context.Tests
                .Include(x => x.Questions)
                .ThenInclude(x => x.Card)
                .SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Test with id {id} could not be found");

        return Task.FromResult(GetTest(test));
    }

    public async Task Add(CreateTestRequest request, CancellationToken cancellationToken = default)
    {
        var cards = _context.Cards.ToList()
            .Where(x => !request.Tags.Any() || x.Tags.Any(t => request.Tags.Contains(t)))
            .Shuffle()
            .Take(request.NumberOfQuestions);

        var questions = cards.Select(x => new Entities.Question {Id = Guid.NewGuid(), Card = x});
        
        var test = new Entities.Test
        {
            Id = request.Id, 
            Prompt = request.Prompt, 
            Questions = questions.ToList()
        };

        _context.Tests.Add(test);

        await _context.SaveChanges(cancellationToken);
    }

    public async Task Update(Guid id, Test test, CancellationToken cancellationToken = default)
    {
        var entity = 
            _context.Tests
                .Include(x => x.Questions)
                .ThenInclude(x => x.Card)
                .SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Test with id {id} could not be found");

        entity.Prompt = test.Prompt;
        foreach (var (card, correct) in test.Questions)
        {
            var question = entity.Questions.Single(q => q.Card.Id == card.Id);
            question.Correct = correct;
        }

        await _context.SaveChanges(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var entity =
            _context.Tests.SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Test with id {id} could not be found");

        _context.Tests.Remove(entity);

        await _context.SaveChanges(cancellationToken);
    }

    private static Test GetTest(Entities.Test test) => new (test.Id, test.Prompt, GetQuestions(test));

    private static IEnumerable<Question> GetQuestions(Entities.Test test) =>
        test.Questions.Select(q => new Question(GetCard(q.Card), q.Correct));

    private static Card GetCard(Entities.Card card) => new (card.Id, card.Front, card.Back, card.Tags);
}
