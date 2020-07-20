using System.Collections.Generic;

class AssetPath
{
    public static readonly string Coin = "Coin/Coin";
    public static readonly string Character = "Character/Character";

    public static readonly Dictionary<PlatformType, string> Platforms =
        new Dictionary<PlatformType, string>
        {
            {PlatformType.Simple, "Platforms/Simple" },
            {PlatformType.Brokening, "Platforms/Brokening" },
            {PlatformType.Moving, "Platforms/Moving" },
            {PlatformType.Trap, "Platforms/Trap" }
        };
    
    public static readonly Dictionary<BoostType, string> Boosts =
        new Dictionary<BoostType, string>
        {
            {BoostType.Trampoline, "Boosts/Trampoline" },
            {BoostType.Jetpack, "Boosts/Jetpack" },
            {BoostType.Magnet, "Boosts/Magnet" },
            {BoostType.WeaponLaser, "Boosts/WeaponLaser" },
            {BoostType.WeaponRocket, "Boosts/WeaponRocket" },
            {BoostType.Armor, "Boosts/Armor" }
        };

    public static readonly Dictionary<EnemyType, string> Enemys =
        new Dictionary<EnemyType, string>
        {
            {EnemyType.Barrier, "Enemys/Barrier" },
            {EnemyType.Pusher, "Enemys/Pusher" },
            {EnemyType.Hit, "Enemys/Hit" }
        };

    public static readonly Dictionary<WeaponType, string> Projectiles =
        new Dictionary<WeaponType, string>
        {
            {WeaponType.Gun, "Projectiles/Bullet" },
            {WeaponType.Laser, "Projectiles/Laser" },
            {WeaponType.Rocket, "Projectiles/Rocket" },
            {WeaponType.Explosion, "Projectiles/Explosion" }
        };

    public static readonly Dictionary<BorderType, string> Borders =
        new Dictionary<BorderType, string>
        {
            {BorderType.Bottom, "Borders/Bottom" },
            {BorderType.Upper, "Borders/Upper" },
            {BorderType.EndLevel, "Borders/EndLevel" },
            {BorderType.Side, "Borders/Side" }
        };

}
