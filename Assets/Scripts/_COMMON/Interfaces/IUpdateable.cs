public interface IUpdateable
{
    bool IsActive { get; }
    void Update();
    void FixedUpdate();
}
