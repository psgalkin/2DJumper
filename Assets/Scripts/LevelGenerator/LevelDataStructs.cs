using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformData
{
    public PlatformType Type;
    public int Ratio;
}

[Serializable]
public class PlatformInterval
{
    public float IntervalStart;
    public float IntervalEnd;
    public float MinDist;
    public float MaxDist;
    public float MovingPlatformSpeed = 1;
    public PlatformData[] PlatformsRatio;
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
    public int StrikerAttackSpeed;
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
