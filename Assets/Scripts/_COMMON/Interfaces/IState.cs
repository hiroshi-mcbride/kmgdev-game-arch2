public interface IState
{
    Scratchpad OwnerData { get; }
    void OnEnter();
    void OnUpdate();
    void OnFixedUpdate();
    void OnExit();
}
