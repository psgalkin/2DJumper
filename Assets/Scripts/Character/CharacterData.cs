using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData")]
public class CharacterData : ScriptableObject
{
    public float ForceJumpCoef;
    public int JetpackDuration;
    public float FlyingSpeed;
    public int MagnetDuration;
    public float CoinSpeed;
    public int ArmorDuration;
}
