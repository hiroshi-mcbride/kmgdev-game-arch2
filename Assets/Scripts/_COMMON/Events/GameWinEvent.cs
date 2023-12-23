public struct GameWinEvent
{
    public float TimeRemaining { get; }
    
    public GameWinEvent(float _timeRemaining)
    {
        TimeRemaining = _timeRemaining;
    }
}
