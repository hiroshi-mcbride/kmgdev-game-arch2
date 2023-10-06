using System;

public class ScoreCounter
{
    private Action<ScoreIncreaseEvent> onScoreIncreaseEventHandler;
    public float TotalScore { get; private set; }

    public ScoreCounter()
    {
        onScoreIncreaseEventHandler = OnScoreIncrease;
        EventManager.Subscribe(typeof(ScoreIncreaseEvent), onScoreIncreaseEventHandler);
    }

    private void OnScoreIncrease(ScoreIncreaseEvent _event)
    {
        TotalScore += _event.Score;
    }

    ~ScoreCounter()
    {
        EventManager.Unsubscribe(typeof(ScoreIncreaseEvent), onScoreIncreaseEventHandler);
    }
}
