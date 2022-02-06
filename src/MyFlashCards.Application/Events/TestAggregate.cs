using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Events;

public class TestAggregate
{ 
    public Guid Id { get; private set; }
    
    public Prompt Prompt { get; private set; }

    public IEnumerable<Question> Questions { get; private set; } = Enumerable.Empty<Question>();
    
    public bool IsDeleted { get; private set; }
    
    private void Apply(TestEvent testEvent)
    {
        switch (testEvent)
        {
            case TestCreatedEvent createdEvent:
                ApplyCreatedEvent(createdEvent);
                break;
            case TestUpdatedEvent updatedEvent:
                ApplyUpdatedEvent(updatedEvent);
                break;
            case TestDeletedEvent deletedEvent:
                ApplyDeletedEvent(deletedEvent);
                break;
        }
    }

    private void ApplyCreatedEvent(TestCreatedEvent createdEvent)
    {
        if (Id != Guid.Empty)
        {
            throw new InvalidOperationException("Applying test created event on an already created aggregate");
        }

        var data = createdEvent.Data;

        Id = data.Id;
        Prompt = data.Prompt;
        Questions = data.Questions;
    }

    private void ApplyUpdatedEvent(TestUpdatedEvent updatedEvent)
    {
        if (updatedEvent.StreamId != Id)
        {
            throw new InvalidOperationException("Applying test updated event on the wrong aggregate");
        }

        var data = updatedEvent.Data;

        Prompt = data.Prompt;
        Questions = data.Questions;
    }

    private void ApplyDeletedEvent(TestDeletedEvent deletedEvent)
    {
        if (deletedEvent.StreamId != Id)
        {
            throw new InvalidOperationException("Applying test deleted event on the wrong aggregate");
        }

        IsDeleted = true;
    }

    public static TestAggregate Load(IEnumerable<TestEvent> events)
    {
        var aggregate = new TestAggregate();
        foreach (var testEvent in events)
        {
            aggregate.Apply(testEvent);
        }

        return aggregate;
    }
}
