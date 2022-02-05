using Microsoft.AspNetCore.Mvc;
using MyFlashCards.Application.Interfaces;
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

    private IResult GetCards(ICardReadRepository repository)
    {
        var cards = repository.Get();
        
        return Results.Ok(cards);
    }

    private async Task<IResult> AddCard([FromBody] Card card, ICardWriteRepository repository, CancellationToken cancellationToken)
    {
        await repository.Add(card, cancellationToken);
        
        return Results.NoContent();
    }

    private async Task<IResult> UpdateCard(Guid id, [FromBody] Card card, ICardWriteRepository repository, CancellationToken cancellationToken)
    {
        await repository.Update(id, card, cancellationToken);
        
        return Results.NoContent();
    }

    private async Task<IResult> DeleteCard(Guid id, ICardWriteRepository repository, CancellationToken cancellationToken)
    {
        await repository.Delete(id, cancellationToken);
        
        return Results.NoContent();
    }
}
