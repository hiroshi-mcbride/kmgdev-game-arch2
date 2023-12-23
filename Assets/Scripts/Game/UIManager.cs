using System;
using UnityEngine;
using TMPro;

public class UIManager : IUpdateable
{
    public bool IsActive { get; set; } = true;
    
    private const string TIME_PREFIX = "Time left: ";
    private const string ENEMIES_PREFIX = "Enemies remaining: ";
    private GameObject playContainer;
    private GameObject winContainer;
    private GameObject loseContainer;

    private TMP_Text playText;
    private string enemiesString;
    private int enemyCount;

    private TMP_Text winText;
    private TMP_Text loseText;

    private Timer gameTimer;
    private Action<EnemyCountChangedEvent> onEnemyCountChangedEventHandler;
    private Action<GameStartEvent> onGameStartEventHandler;
    private Action<GameWinEvent> onGameWinEventHandler;
    private Action<GameLoseEvent> onGameLoseEventHandler;
    
    public UIManager(CanvasItems _canvasItems)
    {
        playContainer = _canvasItems.Play;
        playText = playContainer.GetComponentInChildren<TMP_Text>();
        
        winContainer = _canvasItems.Win;
        winText = winContainer.GetComponentInChildren<TMP_Text>();
        
        loseContainer = _canvasItems.Lose;
        loseText = loseContainer.GetComponentInChildren<TMP_Text>();
        
        EventManager.Invoke(new UpdateableCreatedEvent(this));
        
        onEnemyCountChangedEventHandler = OnEnemyCountChanged;
        EventManager.Subscribe(typeof(EnemyCountChangedEvent), onEnemyCountChangedEventHandler);

        onGameStartEventHandler = OnGameStart;
        EventManager.Subscribe(typeof(GameStartEvent), onGameStartEventHandler);

        onGameWinEventHandler = OnGameWin;
        EventManager.Subscribe(typeof(GameWinEvent), onGameWinEventHandler);
        
        onGameLoseEventHandler = OnGameLose;
        EventManager.Subscribe(typeof(GameLoseEvent), onGameLoseEventHandler);
    }

    public void Update()
    {
        if (gameTimer == null)
        {
            return;
        }
        
        /*
         * TimeSpan code from Microsoft .NET documentation
         * https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromseconds
         */
        TimeSpan interval = TimeSpan.FromSeconds(gameTimer.TimeRemaining);
        string timeString = TIME_PREFIX + interval.ToString(@"mm\:ss\:fff");
        playText.text = enemiesString + "\n" + timeString;
    }

    public void FixedUpdate()
    {
    }

    private void OnGameStart(GameStartEvent _event)
    {
        winContainer.SetActive(false);
        loseContainer.SetActive(false);
        playContainer.SetActive(true);
        gameTimer = _event.GameTimer;
    }

    private void OnGameWin(GameWinEvent _event)
    {
        winContainer.SetActive(true);
        loseContainer.SetActive(false);
        playContainer.SetActive(false);
    }
    
    private void OnGameLose(GameLoseEvent _event)
    {
        winContainer.SetActive(false);
        loseContainer.SetActive(true);
        playContainer.SetActive(false);
    }
    
    private void OnEnemyCountChanged(EnemyCountChangedEvent _event)
    {
        enemyCount = _event.NewCount;
        enemiesString = ENEMIES_PREFIX + enemyCount;
    }

    ~UIManager()
    {
        EventManager.Unsubscribe(typeof(EnemyCountChangedEvent), onEnemyCountChangedEventHandler);
        EventManager.Unsubscribe(typeof(GameStartEvent), onGameStartEventHandler);
        EventManager.Unsubscribe(typeof(GameLoseEvent), onGameLoseEventHandler);
    }
}

public struct CanvasItems
{
    public GameObject Play;
    public GameObject Win;
    public GameObject Lose;
}