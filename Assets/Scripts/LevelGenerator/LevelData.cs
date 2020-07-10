using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] public float LevelHeight;
    [SerializeField] public PlatformInterval[] PlatformIntervals;
    [SerializeField] public CoinInterval[] CoinIntervals;
    [SerializeField] public EnemyInterval[] EnemyIntervals;
    [SerializeField] public BoostInterval[] BoostIntervals;
}
