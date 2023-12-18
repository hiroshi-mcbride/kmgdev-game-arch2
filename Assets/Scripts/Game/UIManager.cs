using System;
using UnityEngine;
using TMPro;

public class UIManager
{
    private TMP_Text timeText;
    private TMP_Text enemiesText;
    private const string TIME_PREFIX = "Time left: ";
    private const string ENEMIES_PREFIX = "Enemies remaining: ";

    private Timer gameTimer;
    private Action<EnemyCountChangedEvent> onEnemyCountChangedEventHandler;
    
    public UIManager(TMP_Text _timeText, TMP_Text _enemiesText, Timer _gameTimer)
    {
        timeText = _timeText;
        enemiesText = _enemiesText;

        gameTimer = _gameTimer;
        
        onEnemyCountChangedEventHandler = OnEnemyCountChanged;
        EventManager.Subscribe(typeof(EnemyCountChangedEvent), onEnemyCountChangedEventHandler);
    }

    private void OnEnemyCountChanged(EnemyCountChangedEvent _event)
    {
        enemiesText.text = ENEMIES_PREFIX + _event.NewCount;
    }
}
