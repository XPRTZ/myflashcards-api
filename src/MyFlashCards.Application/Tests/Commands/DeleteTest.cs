using FluentValidation;
using MediatR;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Tests.Commands;

public record DeleteTest(Guid Id) : IRequest;

public class DeleteTestHandler : IRequestHandler<DeleteTest>
{
    private readonly IFlashCardsContext _context;

    public DeleteTestHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(DeleteTest request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var testExits = _context.Events.ToList()
            .OfType<TestEvent>()
            .Any(x => x.StreamId == id);

        if (!testExits)
        {
            throw new NotFoundException($"Test with id {id} could not be found");
        }

        _context.Events.Add(new TestDeletedEvent(new Test(request.Id, Prompt.Back, Enumerable.Empty<Question>())));

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class DeleteTestValidator : AbstractValidator<DeleteTest>
{
    public DeleteTestValidator() => RuleFor(x => x.Id).NotEmpty();
}
