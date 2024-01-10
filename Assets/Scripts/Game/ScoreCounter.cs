using System;
using UnityEngine;

// CUT FROM THE GAME: added no gameplay value in its current state
public class ScoreCounter
{
    private Action<ScoreIncreaseEvent> onScoreIncreaseEventHandler;
    public float TotalScore { get; private set; }

    public ScoreCounter()
    {
        onScoreIncreaseEventHandler = OnScoreIncrease;
        EventManager.Subscribe(typeof(ScoreIncreaseEvent), onScoreIncreaseEventHandler);
    }

    public void ClearScore() => TotalScore = 0;

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
