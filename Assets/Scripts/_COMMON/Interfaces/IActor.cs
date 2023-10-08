using System;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{ 
    GameObject SceneObject { get; }
    Dictionary<Type, object> Components { get; }

    T GetComponent<T>();
}
