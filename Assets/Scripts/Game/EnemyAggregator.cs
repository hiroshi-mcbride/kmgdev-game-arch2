using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finds all GameObjects in the scene with tag "Enemy" and attaches each to an Enemy Actor
/// </summary>

public class EnemyAggregator
{
    public static List<Enemy> AggregateAll()
    {
        List<Enemy> enemies = new();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in objects)
        {
            enemies.Add(new Enemy(gameObject));
        }

        return enemies;
    }
}
