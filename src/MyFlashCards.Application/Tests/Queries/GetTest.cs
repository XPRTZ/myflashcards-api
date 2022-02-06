using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Tests.Queries;

public record GetTest(Guid Id) : IRequest<Test>;

public class GetTestHandler : IRequestHandler<GetTest, Test>
{
    private readonly IFlashCardsContext _context;

    public GetTestHandler(IFlashCardsContext context) => _context = context;

    public Task<Test> Handle(GetTest request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var testEvents = _context.Events.ToList()
            .OfType<TestEvent>()
            .Where(x => x.StreamId == id)
            .OrderBy(x => x.StreamPosition)
            .ToList();

        if (!testEvents.Any())
        {
            throw new NotFoundException($"Test with id {id} could not be found");
        }

        var aggregate = TestAggregate.Load(testEvents);

        if (aggregate.IsDeleted)
        {
            throw new NotFoundException($"Test with id {id} has been deleted");
        }
        
        return Task.FromResult(new Test(aggregate.Id, aggregate.Prompt, aggregate.Questions));
    }
}

public class GetTestValidator : AbstractValidator<GetTest>
{
    public GetTestValidator() => RuleFor(x => x.Id).NotEmpty();
}
