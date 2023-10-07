using System;
using System.Collections.Generic;

public class UpdateManager
{
    private List<IUpdateable> updateables = new();
    private Queue<IUpdateable> createdObjectBuffer = new();
    private Queue<IUpdateable> destroyedObjectBuffer = new();
    private Action<UpdateableCreatedEvent> onUpdateableCreatedEventHandler;
    private Action<UpdateableDestroyedEvent> onUpdateableDestroyedEventHandler;

    public UpdateManager()
    {
        onUpdateableCreatedEventHandler = OnUpdateableCreated;
        onUpdateableDestroyedEventHandler = OnUpdateableDestroyed;
        EventManager.Subscribe(typeof(UpdateableCreatedEvent), onUpdateableCreatedEventHandler);
        EventManager.Subscribe(typeof(UpdateableDestroyedEvent), onUpdateableDestroyedEventHandler);
    }

    public void UpdateAll()
    {
        foreach (IUpdateable updateable in updateables)
        {
            if (updateable.IsActive) 
            { 
                updateable.Update(); 
            }
        }
    }

    public void FixedUpdateAll()
    {
        foreach (IUpdateable updateable in updateables)
        {
            if (updateable.IsActive)
            {
                updateable.FixedUpdate();
            }
        }
    }

    public void ProcessBuffer()
    {
        while (createdObjectBuffer.Count > 0)
        {
            IUpdateable bufferedItem = createdObjectBuffer.Dequeue();
            updateables.Add(bufferedItem);
        }

        while (destroyedObjectBuffer.Count > 0)
        {
            IUpdateable bufferedItem = destroyedObjectBuffer.Dequeue();
            updateables.Remove(bufferedItem);
        }
    }
    
    private void OnUpdateableCreated(UpdateableCreatedEvent _event)
    {
        createdObjectBuffer.Enqueue(_event.CreatedObject);
    }

    private void OnUpdateableDestroyed(UpdateableDestroyedEvent _event)
    {
        destroyedObjectBuffer.Enqueue(_event.DestroyedObject);
    }
    ~UpdateManager()
    {
        EventManager.Unsubscribe(typeof(UpdateableCreatedEvent), onUpdateableCreatedEventHandler);
        EventManager.Unsubscribe(typeof(UpdateableDestroyedEvent), onUpdateableDestroyedEventHandler);
    }
}
