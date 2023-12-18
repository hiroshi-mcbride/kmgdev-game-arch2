using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes and manages all GameObjects tagged "Enemy" as Enemy Actors
/// </summary>

public class EnemyManager
{
    private List<Enemy> enemies = new();
    private Action<EnemyKilledEvent> onEnemyKilledEventHandler;
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
        onEnemyKilledEventHandler = OnEnemyKilled;
        EventManager.Subscribe(typeof(EnemyKilledEvent), onEnemyKilledEventHandler);
    }


    public void AggregateAll()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in objects)
        {
            enemies.Add(new Enemy(gameObject));
        }

        EnemyCount = enemies.Count;
    }

    public void InitializeAll(EnemyData _enemyData)
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Initialize(_enemyData);
        }
    }

    private void OnEnemyKilled(EnemyKilledEvent _event)
    {
        enemies.Remove(_event.KilledEnemy);
        EnemyCount--;
        EventManager.Invoke(new EnemyCountChangedEvent(EnemyCount));
    }

    ~EnemyManager()
    {
        EventManager.Unsubscribe(typeof(EnemyKilledEvent), onEnemyKilledEventHandler);
    }
}
