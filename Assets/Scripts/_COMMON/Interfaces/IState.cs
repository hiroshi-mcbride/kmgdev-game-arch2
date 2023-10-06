public interface IState
{
    Scratchpad OwnerData { get; }
    void Enter();
    void Update(float _delta);
    void FixedUpdate(float _fixedDelta);
    void Exit();
}
