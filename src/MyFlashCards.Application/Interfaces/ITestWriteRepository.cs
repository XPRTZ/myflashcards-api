using MyFlashCards.Domain.Models;
using MyFlashCards.Domain.Requests;

namespace MyFlashCards.Application.Interfaces;

public interface ITestWriteRepository
{
    Task Add(CreateTestRequest request, CancellationToken cancellationToken = default);
    Task Update(Guid id, Test test, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}
