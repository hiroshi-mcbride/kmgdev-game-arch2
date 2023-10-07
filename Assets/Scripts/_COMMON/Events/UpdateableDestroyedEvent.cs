public struct UpdateableDestroyedEvent
{
    public IUpdateable DestroyedObject { get; }

    public UpdateableDestroyedEvent(IUpdateable _destroyedObject)
    {
        DestroyedObject = _destroyedObject;
    }
}
