using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Initializes and manages all GameObjects tagged "Enemy" as Enemy Actors
/// </summary>

public class EnemyManager
{
    private List<Enemy> enemies = new();
    private Action<EnemyKillEvent> onEnemyKillEventHandler;
    private int enemyCount;
    private int EnemyCount
    {
        get => enemyCount;
        set
        {
            if (value != enemyCount)
            {
                if (value == 0)
                {
                    EventManager.Invoke(new AllEnemiesKilledEvent());
                }
                enemyCount = value;
            }
        }
    }

    public EnemyManager()
    {
        onEnemyKillEventHandler = OnEnemyKilled;
        EventManager.Subscribe(typeof(EnemyKillEvent), onEnemyKillEventHandler);
    }


    public void AggregateAll()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in objects)
        {
            enemies.Add(new Enemy(gameObject));
        }

        EnemyCount = enemies.Count;
        EventManager.Invoke(new EnemyCountChangedEvent(EnemyCount));
    }

    public void InitializeAll(EnemyData _enemyData)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Initialize(_enemyData);
        }
    }

    private void OnEnemyKilled(EnemyKillEvent _event)
    {
        enemies.Remove(_event.KilledEnemy);
        EnemyCount--;
        EventManager.Invoke(new EnemyCountChangedEvent(EnemyCount));
    }

    ~EnemyManager()
    {
        EventManager.Unsubscribe(typeof(EnemyKillEvent), onEnemyKillEventHandler);
    }
}
