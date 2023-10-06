using System;
using System.Collections.Generic;
using System.Linq;

public class UpdateManager
{
    private Dictionary<int, IUpdateable> updateables = new();
    private static int currentId = -1;
    private Func<UpdateableCreatedEvent, int> onUpdateableCreatedEventHandler;

    public UpdateManager()
    {
        onUpdateableCreatedEventHandler = OnUpdateableCreated;
        EventManager.Subscribe(typeof(UpdateableCreatedEvent), onUpdateableCreatedEventHandler);
    }

    public void UpdateAll(float _delta)
    {
        foreach (KeyValuePair<int, IUpdateable> updateable in updateables)
        {
            updateable.Value.Update(_delta);
        }
    }

    public void FixedUpdateAll(float _fixedDelta)
    {
        foreach (KeyValuePair<int, IUpdateable> updateable in updateables)
        {
            updateable.Value.FixedUpdate(_fixedDelta);
        }
    }
    
    private int OnUpdateableCreated(UpdateableCreatedEvent _event)
    {
        currentId++;
        updateables.Add(currentId, _event.CreatedObject);
        return currentId;
    }

    ~UpdateManager()
    {
        EventManager.Unsubscribe(typeof(UpdateableCreatedEvent), onUpdateableCreatedEventHandler);
    }
}
