using Microsoft.AspNetCore.Mvc;
using MyFlashCards.Domain;
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
            .Produces(StatusCodes.Status201Created)
            .ProducesErrorCodes();

        app.MapDelete("/cards/{id}", DeleteCard)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesErrorCodes();
    }

    private async Task<IResult> GetCards(CancellationToken cancellationToken) => 
        throw new NotImplementedException();

    private async Task<IResult> AddCard([FromBody] Card card, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    private async Task<IResult> UpdateCard(Guid id, [FromBody] Card card, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    private async Task<IResult> DeleteCard(Guid id, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
