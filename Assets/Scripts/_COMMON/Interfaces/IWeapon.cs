public interface IWeapon
{
    bool IsAutomatic { get; }
    void Fire();
    void Update(float _delta);
}
