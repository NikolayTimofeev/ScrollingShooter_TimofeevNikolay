namespace Assets.ScrollingShooter.Scripts.Weapons
{
    public interface IWeapon
    {
        void Shoot();

        void AutoFireStart();
        void AutoFireStop();
    }
}