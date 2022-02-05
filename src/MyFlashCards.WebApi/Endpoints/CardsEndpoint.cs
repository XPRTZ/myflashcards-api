using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyFlashCards.Application.Cards.Commands;
using MyFlashCards.Application.Cards.Queries;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.WebApi.Endpoints;

public class CardsEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/cards", GetCards)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(IEnumerable<Card>))
            .ProducesErrorCodes();

        app.MapPost("/cards", AddCard)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesErrorCodes();

        app.MapPut("/cards/{id}", UpdateCard)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesErrorCodes();

        app.MapDelete("/cards/{id}", DeleteCard)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesErrorCodes();
    }

    private async Task<IResult> GetCards(IMediator mediator, CancellationToken cancellationToken)
    {
        var cards = await mediator.Send(new GetCards(), cancellationToken);
        
        return Results.Ok(cards);
    }

    private async Task<IResult> AddCard([FromBody] Card card, IMediator mediator, CancellationToken cancellationToken)
    {
        await mediator.Send(new AddCard(card), cancellationToken);
        
        return Results.NoContent();
    }

    private async Task<IResult> UpdateCard(Guid id, [FromBody] Card card, IMediator mediator, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateCard(id, card), cancellationToken);
        
        return Results.NoContent();
    }

    private async Task<IResult> DeleteCard(Guid id, IMediator mediator, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCard(id), cancellationToken);
        
        return Results.NoContent();
    }
}
