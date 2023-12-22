public struct EnemyKillEvent
{
    public Enemy KilledEnemy { get; }
    public EnemyKillEvent(Enemy _killedEnemy)
    {
        KilledEnemy = _killedEnemy;
    }
}
