using MyFlashCards.Application.Tests.Commands;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Events;

public class TestEvent : Event<Test>
{
}

public class TestCreatedEvent : TestEvent
{
    public TestCreatedEvent()
    {
        
    }
    
    public TestCreatedEvent(Test data)
    {
        Data = data;
        StreamId = data.Id;
    }
}

public class TestUpdatedEvent : TestEvent
{
    public TestUpdatedEvent()
    {
        
    }
    
    public TestUpdatedEvent(Test data)
    {
        Data = data;
        StreamId = data.Id;
    }
}

public class TestDeletedEvent : TestEvent
{
    public TestDeletedEvent()
    {
        
    }
    
    public TestDeletedEvent(Test data)
    {
        Data = data;
        StreamId = data.Id;
    }
}
