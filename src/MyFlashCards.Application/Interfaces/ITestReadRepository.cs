using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Interfaces;

public interface ITestReadRepository
{
    Test Get(Guid id);
}
