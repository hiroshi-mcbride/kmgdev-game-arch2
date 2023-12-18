public struct EnemyCountChangedEvent
{
    public int NewCount { get; }

    public EnemyCountChangedEvent(int _newCount)
    {
        NewCount = _newCount;
    }
}
