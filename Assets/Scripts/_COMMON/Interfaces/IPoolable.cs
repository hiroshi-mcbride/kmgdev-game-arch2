public interface IPoolable
{
    bool IsActive { get; set; }
    void OnEnableObject();
    void OnDisableObject();
}