using UnityEngine;

public class CharacterLogic : MonoBehaviour
{
    [SerializeField] CharacterData _data;

    public void UpMagnetWorkTime(int incVal, int coinCost)
    {
        if (_data.CoinCount > coinCost)
        {
            _data.CoinCount -= coinCost;
            _data.MagnetDuration += incVal;
        }
    }

}
