using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Usable by any class to store data of any kind
/// </summary>
public class Scratchpad
{
    private Dictionary<string, object> pad = new();

    public void Write(string _name, object _data, bool _overwrite = false)
    {
        bool keyIsAvailable = pad.TryAdd(_name, _data);
        
        if (!keyIsAvailable)
        {
            if (_overwrite)
            {
                pad[_name] = _data;
            }
            else
            {
                Debug.LogWarning($"Can't write: Scratchpad already contains entry with name {_name}. " +
                                 $"Overwrite is false.");
            }
        }
    }
    
    public T Read<T>(string _name)
    {
        bool containsValue = pad.TryGetValue(_name, out object value);
        
        if (!containsValue)
        {
            Debug.LogError($"Can't read: Scratchpad does not contain entry with name {_name}.");
            return default;
        }

        if (value == null)
        {
            Debug.LogError($"Can't read: Scratchpad entry with name {_name} is null.");
            return default;
        }

        if (value.GetType() != typeof(T))
        {
            Debug.LogError($"Can't read: Type mismatch between {typeof(T)} and entry with name {_name}.");
            return default;
        }

        return (T)value;
    }

    public void Delete(string _name)
    {
        bool containsType = pad.ContainsKey(_name);
        
        if (containsType)
        {
            pad.Remove(_name);
        }
        else
        {
            Debug.LogWarning($"Can't delete: Scratchpad does not contain entry with name {_name}.");
        }
    }
}
