using System;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Initializes and manages all GameObjects tagged "Enemy" as Enemy Actors
/// </summary>

public class EnemyManager
{
    private List<Enemy> livingEnemies = new();
    private List<Enemy> killedEnemies = new();
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

    public void InitializeAll(EnemyData _enemyData)
    {
        AggregateAll();
        foreach (Enemy enemy in livingEnemies)
        {
            enemy.Initialize(_enemyData);
            enemy.IsActive = true;
        }
    }

    private void AggregateAll()
    {
        GameObject[] foundGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject gameObject in foundGameObjects)
        {
            bool exists = ActorDirectory.TryLocate(gameObject, out IActor actor);
            
            if (exists && actor is Enemy enemy)
            {
                if (!livingEnemies.Contains(enemy))
                {
                    livingEnemies.Add(enemy);
                }

                if (killedEnemies.Contains(enemy))
                {
                    killedEnemies.Remove(enemy);
                }
            }
            else
            {
                livingEnemies.Add(new Enemy(gameObject));
            }
        }

        foreach (Enemy e in killedEnemies)
        {
            livingEnemies.Add(e);
        }
        killedEnemies.Clear();
        
        EnemyCount = livingEnemies.Count;
        EventManager.Invoke(new EnemyCountChangedEvent(EnemyCount));
    }

    private void OnEnemyKilled(EnemyKillEvent _event)
    {
        killedEnemies.Add(_event.KilledEnemy);
        livingEnemies.Remove(_event.KilledEnemy);
        EnemyCount--;
        EventManager.Invoke(new EnemyCountChangedEvent(EnemyCount));
    }

    ~EnemyManager()
    {
        EventManager.Unsubscribe(typeof(EnemyKillEvent), onEnemyKillEventHandler);
    }
}
