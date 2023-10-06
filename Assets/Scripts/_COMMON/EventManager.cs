using System;
using System.Collections.Generic;

public class EventManager
{
    private static Dictionary<Type, Delegate> eventDictionary = new();
    
    public static void Subscribe(Type _type, Delegate _function)
    {
        eventDictionary.TryAdd(_type, null);
        eventDictionary[_type] = Delegate.Combine(eventDictionary[_type], _function);
    }

    public static void Unsubscribe(Type _type, Delegate _function)
    {
        if (!eventDictionary.ContainsKey(_type) || eventDictionary[_type] == null)
        {
            return;
        }
        eventDictionary[_type] = Delegate.Remove(eventDictionary[_type], _function);
    }

    public static void Invoke(object _event)
    {
        if (!eventDictionary.ContainsKey(_event.GetType()))
        {
            return;
        }

        eventDictionary[_event.GetType()]?.DynamicInvoke(_event);
    }

    public static bool InvokeCallback(object _event, out object _callback)
    {
        if (!eventDictionary.ContainsKey(_event.GetType()))
        {
            _callback = null;
            return false;
        }
        _callback = eventDictionary[_event.GetType()]?.DynamicInvoke(_event);
        return _callback != null;
    }
}
