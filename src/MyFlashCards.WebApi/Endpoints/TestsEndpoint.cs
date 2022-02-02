using Microsoft.AspNetCore.Mvc;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;
using MyFlashCards.Domain.Requests;

namespace MyFlashCards.WebApi.Endpoints;

public class TestsEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/tests/{id}", GetTest)
            .AllowAnonymous()
            .Produces(StatusCodes.Status200OK, typeof(Test))
            .ProducesErrorCodes();

        app.MapPost("/tests", AddTest)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesErrorCodes();

        app.MapPut("/tests/{id}", UpdateTest)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesErrorCodes();

        app.MapDelete("/tests/{id}", DeleteTest)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesErrorCodes();
    }

    private async Task<IResult> GetTest(Guid id, ITestRepository repository, CancellationToken cancellationToken)
    {
        var test = await repository.Get(id, cancellationToken);
        
        return Results.Ok(test);
    }

    private async Task<IResult> AddTest([FromBody] CreateTestRequest request, ITestRepository repository, CancellationToken cancellationToken)
    {
        await repository.Add(request, cancellationToken);
        
        return Results.NoContent();
    }

    private async Task<IResult> UpdateTest(Guid id, [FromBody] Test test, ITestRepository repository, CancellationToken cancellationToken)
    {
        await repository.Update(id, test, cancellationToken);
        
        return Results.NoContent();
    }

    private async Task<IResult> DeleteTest(Guid id, ITestRepository repository, CancellationToken cancellationToken)
    {
        await repository.Delete(id, cancellationToken);
        
        return Results.NoContent();
    }
}
