
public class ServiceLocator<T>
{
    private static T service;

    public static void Provide(T _service)
    {
        service = _service;
    }
    
    public static T Locate()
    {
        if (service == null)
        {
            service = default;
        }
        return service;
    }
}
