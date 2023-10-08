using UnityEngine;

public class Enemy : BaseActor, IDamageable
{
    public float Health { get; private set; }
    public float PointValue { get; private set; }

    public Enemy(GameObject _instance)
    {
        SceneObject = _instance;
        base.InitializeActor();
    }
    public void Initialize(EnemyData _enemyData)
    {
        Health = _enemyData.Health;
        PointValue = _enemyData.PointValue;
    }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
        if (Health <= 0)
        {
            IsActive = false;
            EventManager.Invoke(new ScoreIncreaseEvent(PointValue));
        }
    }

}
