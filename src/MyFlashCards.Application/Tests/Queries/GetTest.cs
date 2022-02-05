using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Tests.Queries;

public record GetTest(Guid Id) : IRequest<Test>;

public class GetTestHandler : IRequestHandler<GetTest, Test>
{
    private IFlashCardsContext _context;

    public GetTestHandler(IFlashCardsContext context) => _context = context;

    public Task<Test> Handle(GetTest request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var test =
            _context.Tests
                .Include(x => x.Questions)
                .ThenInclude(x => x.Card)
                .SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Test with id {id} could not be found");

        return Task.FromResult(GetTest(test));
    }

    private static Test GetTest(Entities.Test test) => new(test.Id, test.Prompt, GetQuestions(test));

    private static IEnumerable<Question> GetQuestions(Entities.Test test) =>
        test.Questions.Select(q => new Question(GetCard(q.Card), q.Correct));

    private static Card GetCard(Entities.Card card) => new(card.Id, card.Front, card.Back, card.Tags);
}

public class GetTestValidator : AbstractValidator<GetTest>
{
    public GetTestValidator() => RuleFor(x => x.Id).NotEmpty();
}
