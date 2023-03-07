namespace Game
{
    public class GrenadeLauncher : RangedWeapon
    {
        protected override void Initialize()
        {
            OnItemUseDown += Launch;
        }
    }
}