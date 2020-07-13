using UnityEngine;
using TMPro;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _boostText;

    public void SetCoinCount(int count)
    {
        _coinText.text = $"Coins: {count}";
    }

    public void BoostStatus(string msg)
    {
        _boostText.text = msg;
    }
}
