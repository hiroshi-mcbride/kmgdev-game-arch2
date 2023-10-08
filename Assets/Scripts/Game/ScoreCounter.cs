using System;
using UnityEngine;

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
        Debug.Log($"Score increased by {_event.Score}! new score: {TotalScore}");
    }

    ~ScoreCounter()
    {
        EventManager.Unsubscribe(typeof(ScoreIncreaseEvent), onScoreIncreaseEventHandler);
    }
}
