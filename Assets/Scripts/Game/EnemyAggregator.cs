using UnityEngine;

public class EnemyAggregator
{
    public static void AggregateAll()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in objects)
        {
            Enemy enemy = new(gameObject);
        }
    }
}
