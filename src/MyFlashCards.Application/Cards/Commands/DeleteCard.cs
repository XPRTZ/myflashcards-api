using FluentValidation;
using MediatR;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;

namespace MyFlashCards.Application.Cards.Commands;

public record DeleteCard(Guid Id) : IRequest;

public class DeleteCardHandler : IRequestHandler<DeleteCard>
{
    private IFlashCardsContext _context;

    public DeleteCardHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(DeleteCard request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var entity =
            _context.Cards.SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Card with id {id} could not be found");

        _context.Cards.Remove(entity);

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class DeleteCardValidator : AbstractValidator<DeleteCard>
{
    public DeleteCardValidator() => RuleFor(x => x.Id).NotEmpty();
}
