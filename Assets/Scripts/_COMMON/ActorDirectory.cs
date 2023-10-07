using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides global access to a given instance of any object that implements IService
/// </summary>
public static class ActorDirectory
{
    private static Dictionary<GameObject, IActor> directory = new();
    
    public static void Provide(IActor _actor)
    {
        directory.TryAdd(_actor.SceneObject, _actor);
    }

    public static IActor Locate(GameObject _gameObject)
    {
        bool found = directory.TryGetValue(_gameObject, out IActor actor);
        return actor;
    }

    public static void Remove(GameObject _gameObject)
    {
        directory.Remove(_gameObject);
    }
}