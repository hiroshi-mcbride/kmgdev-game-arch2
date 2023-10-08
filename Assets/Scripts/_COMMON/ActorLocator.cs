using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helps to locate any Actor by its GameObject
/// </summary>
public static class ActorLocator
{
    private static Dictionary<GameObject, IActor> directory = new();
    
    public static void Provide(IActor _actor)
    {
        directory.TryAdd(_actor.SceneObject, _actor);
    }

    public static bool TryLocate(GameObject _gameObject, out IActor _actor)
    {
        bool found = directory.TryGetValue(_gameObject, out IActor actor);
        _actor = actor;
        return found;
    }

    public static void Remove(GameObject _gameObject)
    {
        directory.Remove(_gameObject);
    }
}