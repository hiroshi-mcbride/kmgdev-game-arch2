using System;
using System.Collections.Generic;
using System.Linq;

public class UpdateManager
{
    private Dictionary<int, IUpdateable> updateables = new();
    private static int currentId = -1;

    public UpdateManager()
    {

        
        //EventManager.Subscribe(typeof(UpdateableCreatedEvent), onUpdateableCreatedEventHandler);
    }

    public int AddUpdateable(IUpdateable _updateable)
    {
        currentId++;
        updateables.Add(currentId, _updateable);
        return currentId;
    }

    public void UpdateAll(float _delta)
    {
        for (int i = 0; i <= currentId; i++)
        {
            updateables[i]?.Update(_delta);
        }
    }

    // public void FixedUpdateAll(float _fixedDelta)
    // {
    //     foreach (IFixedUpdateable fixedUpdateable in fixedUpdateables)
    //     {
    //         fixedUpdateable.FixedUpdate(_fixedDelta);
    //     }
    // }
}
