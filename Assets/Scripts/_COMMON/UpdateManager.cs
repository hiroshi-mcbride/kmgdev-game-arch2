using System;
using System.Collections.Generic;
using System.Linq;

public class UpdateManager
{
    private static int currentId;
    private Dictionary<int, IUpdateable> updateables = new();
    private Func<UpdateableCreatedEvent, int> onUpdateableCreatedEventHandler;
    private Stack<BufferedItem> createdObjectBuffer = new();
    private Stack<int> destroyedObjectBuffer = new();

    public UpdateManager()
    {
        onUpdateableCreatedEventHandler = OnUpdateableCreated;
        EventManager.Subscribe(typeof(UpdateableCreatedEvent), onUpdateableCreatedEventHandler);
    }

    public void UpdateAll()
    {
        foreach (KeyValuePair<int, IUpdateable> updateable in updateables)
        {
            if (updateable.Value.IsActive) 
            { 
                updateable.Value.Update(); 
            }
        }
    }

    public void FixedUpdateAll()
    {
        foreach (KeyValuePair<int, IUpdateable> updateable in updateables)
        {
            if (updateable.Value.IsActive)
            {
                updateable.Value.FixedUpdate();
            }
        }
    }

    public void ProcessBuffer()
    {
        while (createdObjectBuffer.Count > 0)
        {
            BufferedItem bufferedItem = createdObjectBuffer.Pop();
            updateables.Add(bufferedItem.Id, bufferedItem.Updateable);
        }
    }
    
    private int OnUpdateableCreated(UpdateableCreatedEvent _event)
    {
        currentId++;
        createdObjectBuffer.Push(new BufferedItem(currentId, _event.CreatedObject));
        return currentId;
    }
    ~UpdateManager()
    {
        EventManager.Unsubscribe(typeof(UpdateableCreatedEvent), onUpdateableCreatedEventHandler);
    }

    private struct BufferedItem
    {
        public int Id { get; }
        public IUpdateable Updateable { get; }

        public BufferedItem(int _id, IUpdateable _updateable)
        {
            Id = _id;
            Updateable = _updateable;
        }
    }
}
