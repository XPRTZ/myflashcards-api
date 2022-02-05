using FluentValidation;
using MediatR;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;

namespace MyFlashCards.Application.Tests.Commands;

public record DeleteTest(Guid Id) : IRequest;

public class DeleteTestHandler : IRequestHandler<DeleteTest>
{
    private IFlashCardsContext _context;

    public DeleteTestHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(DeleteTest request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var entity =
            _context.Tests.SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Test with id {id} could not be found");

        _context.Tests.Remove(entity);

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class DeleteTestValidator : AbstractValidator<DeleteTest>
{
    public DeleteTestValidator() => RuleFor(x => x.Id).NotEmpty();
}
