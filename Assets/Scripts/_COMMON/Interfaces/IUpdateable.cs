public interface IUpdateable : IToggleActive
{
    int Id { get; }
    void Update(float _delta);
    void FixedUpdate(float _fixedDelta);
}
