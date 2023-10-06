using System;
using System.Collections.Generic;
/// <summary>
/// Provides global access to a given instance of any object that implements IService
/// </summary>
public static class Services
{
    private static Dictionary<Type, object> services = new();

    public static void Provide<T>(T _service) where T : IService
    {
        Type t = _service.GetType();
        bool keyIsAvailable = services.TryAdd(t, _service);
        if (!keyIsAvailable)
        {
            services[t] = _service;
        }
    }

    public static T Locate<T>() where T : IService
    {
        bool serviceExists = services.TryGetValue(typeof(T), out object value);
        return (T)value;
    }
}