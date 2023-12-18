public struct EnemyKilledEvent
{
    public Enemy KilledEnemy { get; }
    public EnemyKilledEvent(Enemy _killedEnemy)
    {
        KilledEnemy = _killedEnemy;
    }
}
