public struct UpdateableCreatedEvent
{
    public IUpdateable CreatedObject { get; }

    public UpdateableCreatedEvent(IUpdateable _createdObject)
    {
        CreatedObject = _createdObject;
    }
}
