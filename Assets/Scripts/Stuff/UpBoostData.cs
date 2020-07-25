using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UpBoostData")]
class UpBoostData : ScriptableObject
{
    [SerializeField] int _magnetCoast;
    [SerializeField] int _trampolineCoast;
    [SerializeField] int _jetpackCoast;
    [SerializeField] int _armorCoast;

    public int UpMagnetVal;
    public float UpTrampolineVal;
    public int UpJetpackVal;
    public int UpArmorVal;

    public Dictionary<BoostType, int> GetCoastDictionary()
    {
        return new Dictionary<BoostType, int> { 
            {BoostType.Magnet, _magnetCoast}, 
            {BoostType.Trampoline, _trampolineCoast}, 
            {BoostType.Jetpack, _jetpackCoast}, 
            {BoostType.Armor, _armorCoast} };
    }
}
