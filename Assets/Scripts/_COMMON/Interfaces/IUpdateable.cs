public interface IUpdateable : IToggleActive
{
    int Id { get; }
    void Update();
    void FixedUpdate();
}
