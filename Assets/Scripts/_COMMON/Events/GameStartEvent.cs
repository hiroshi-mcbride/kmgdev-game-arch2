public struct GameStartEvent
{
    public Timer GameTimer { get; }

    public GameStartEvent(Timer _gameTimer)
    {
        GameTimer = _gameTimer;
    }
}
