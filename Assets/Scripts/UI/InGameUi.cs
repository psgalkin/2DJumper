using UnityEngine;
using TMPro;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _lossText;

    private StatusArea _statusArea;

    public void SetCoinCount(int count)
    {
        _coinText.text = $"Coins: {count}";
    }

    private void Start()
    {
        _statusArea = GetComponent<StatusArea>();
    }

    public void StartBoostStatus(BoostType type, int time)
    {
        _statusArea.AddField(type, time);
    }

    public void EndBoostStatus(BoostType type)
    {
        _statusArea.RemoveField(type);
    }

    public void SetBoostStatus(BoostType type, int time)
    {
        _statusArea.SetBoostTiming(type, time);
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
