using System.Collections.Generic;

class AssetPath
{
    public static readonly string Coin = "Coin/Coin";
    public static readonly string Character = "Character/Character";
    public static readonly string AudioSourcePrefab = "Sound/AudioSource";
    public class Sounds
    {
        public static readonly string SimpleJump = "Sound/Jump/Simple";
        public static readonly string BrokeningJump = "Sound/Jump/Brokening";
        public static readonly string TrapJump = "Sound/Jump/Trap";
        public static readonly string MovingJump = "Sound/Jump/Moving";

        public static readonly string TrampolineBoost = "Sound/Boost/Trampoline";
        public static readonly string MagnetBoost = "Sound/Boost/Magnet";
        public static readonly string JetpackBoost = "Sound/Boost/Jetpack";
        public static readonly string ArmorBoost = "Sound/Boost/Armor";
        public static readonly string WeaponBoost = "Sound/Boost/Weapon";

        public static readonly string SimpleAttack = "Sound/Attack/Simple";
        public static readonly string LaserAttack = "Sound/Attack/Laser";
        public static readonly string RocketAttack = "Sound/Attack/Rocket";
        public static readonly string Explosion = "Sound/Attack/Explosion";

        public static readonly string BarrierEnemy = "Sound/Enemy/Barrier";
        public static readonly string PusherEnemy = "Sound/Enemy/Pusher";
        public static readonly string EnemyHit = "Sound/Enemy/Hit";

        public static readonly string Coin = "Sound/Coin";
    }

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
