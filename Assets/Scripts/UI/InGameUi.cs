using UnityEngine;
using TMPro;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _boostText;

    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _lossText;

    public void SetCoinCount(int count)
    {
        _coinText.text = $"Coins: {count}";
    }

    public void BoostStatus(string msg)
    {
        _boostText.text = msg;
    }

    public void SetWinText()
    {
        _winText.gameObject.SetActive(true);
    }

    public void SetLossText()
    {
        _lossText.gameObject.SetActive(true);
    }
}
