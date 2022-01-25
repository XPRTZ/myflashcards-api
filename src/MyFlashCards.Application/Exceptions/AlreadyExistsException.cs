namespace MyFlashCards.Application.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string entityName, object entityId) : base($"{entityName} with id {entityId} already exists") { }
}
