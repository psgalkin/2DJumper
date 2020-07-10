using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    None = 0,
    Simple = 1,
    Brokening = 2,
    Moving = 3,
    Trap = 4
}

public enum EnemyType
{
    None = 0,
    Barrier = 1,
    Pusher = 2
}

public enum BoostType
{
    None = 0,
    Jetpack = 1,
    Trampoline = 2,
    Magnet = 3,
    Weapon = 4,
    Armor = 5
}

public enum WeaponType
{
    None = 0,
    Gun = 1,
    Laser = 2,
    Rocket = 3
}

[Serializable]
public class PlatformInterval
{
    public float IntervalStart;
    public float IntervalEnd;
    public float MinDist;
    public float MaxDist;
    public Dictionary<PlatformType, int> PlatformsProbabilitys;
}

[Serializable]
public class CoinInterval
{
    public float IntervalStart;
    public float IntervalEnd;
    public int Number;
}

[Serializable]
public class EnemyIntervalData
{
    public EnemyType EnemyType;
    public int Number;
    [Range(0, 100)] public int Probability;
}

[Serializable]
public class EnemyInterval
{
    public float IntervalStart;
    public float IntervalEnd;
    public EnemyIntervalData[] TypesNumbersProbabilitys;
}

[Serializable]
public class BoostIntervalData
{
    public BoostType BoostType;
    public int Number;
    [Range(0, 100)] public int Probability;
}

[Serializable]
public class BoostInterval
{
    public float IntervalStart;
    public float IntervalEnd;
    public BoostIntervalData[] TypesNumbersProbabilitys;
}
