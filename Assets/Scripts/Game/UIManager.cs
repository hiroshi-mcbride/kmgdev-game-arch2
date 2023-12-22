using System;
using UnityEngine;
using TMPro;

public class UIManager : IUpdateable
{
    public bool IsActive { get; set; } = true;
    
    private const string TIME_PREFIX = "Time left: ";
    private const string ENEMIES_PREFIX = "Enemies remaining: ";
    private TMP_Text timeText;
    private TMP_Text enemiesText;
    private GameObject winCard;
    private GameObject loseCard;

    private Timer gameTimer;
    private Action<EnemyCountChangedEvent> onEnemyCountChangedEventHandler;
    private Action<GameStartEvent> onGameStartEventHandler;
    private Action<GameLoseEvent> onGameLoseEventHandler;
    
    public UIManager(TMP_Text _timeText, TMP_Text _enemiesText)
    {
        timeText = _timeText;
        enemiesText = _enemiesText;
        //gameTimer = _gameTimer;
        
        EventManager.Invoke(new UpdateableCreatedEvent(this));
        
        onEnemyCountChangedEventHandler = OnEnemyCountChanged;
        EventManager.Subscribe(typeof(EnemyCountChangedEvent), onEnemyCountChangedEventHandler);

        onGameStartEventHandler = OnGameStart;
        EventManager.Subscribe(typeof(GameStartEvent), onGameStartEventHandler);
        
        onGameLoseEventHandler = OnGameLose;
        EventManager.Subscribe(typeof(GameLoseEvent), onGameLoseEventHandler);
    }
    
    public void Update()
    {
        if (gameTimer == null)
        {
            Debug.Log("am i even here");
            return;
        }
        /*
         * TimeSpan code from Microsoft .NET documentation
         * https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromseconds
         */
        TimeSpan    interval = TimeSpan.FromSeconds( gameTimer.TimeRemaining );
        string      timeInterval = interval.ToString( @"mm\:ss\:fff");

        // Pad the end of the TimeSpan string with spaces if it 
        // does not contain milliseconds.
        // int pIndex = timeInterval.IndexOf( ':' );
        // pIndex = timeInterval.IndexOf( '.', pIndex );
        // if( pIndex < 0 )   timeInterval += "        ";
        //
        timeText.text = TIME_PREFIX + timeInterval;
    }

    public void FixedUpdate()
    {
    }

    private void OnGameStart(GameStartEvent _event)
    {
        gameTimer = _event.GameTimer;
    }
    
    private void OnGameLose(GameLoseEvent _event)
    {
        
    }
    
    private void OnEnemyCountChanged(EnemyCountChangedEvent _event)
    {
        enemiesText.text = ENEMIES_PREFIX + _event.NewCount;
    }

    ~UIManager()
    {
        EventManager.Unsubscribe(typeof(EnemyCountChangedEvent), onEnemyCountChangedEventHandler);
        EventManager.Unsubscribe(typeof(GameStartEvent), onGameStartEventHandler);
    }
}

public struct CanvasItems
{
    public GameObject Play;
    public GameObject Win;
    public GameObject Lose;

    public TMP_Text EnemiesHUD;
    public TMP_Text TimeHUD;
}