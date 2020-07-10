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
            {BoostType.Weapon, "Boosts/Weapon" },
            {BoostType.Armor, "Boosts/Armor" }
        };

    public static readonly Dictionary<EnemyType, string> Enemys =
        new Dictionary<EnemyType, string>
        {
            {EnemyType.Barrier, "Enemys/Barrier" },
            {EnemyType.Pusher, "Enemys/Pusher" }
        };
}
