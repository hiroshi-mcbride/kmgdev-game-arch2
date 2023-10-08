public class Enemy : BaseActor, IDamageable
{
    public float Health { get; private set; }

    public void TakeDamage(float _damage)
    {
        Health -= _damage;
    }
}
