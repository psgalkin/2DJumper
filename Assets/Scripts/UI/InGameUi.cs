using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private TMP_Text _lossText;

    [SerializeField] private GameObject _pauseUi;
    [SerializeField] private GameObject _endGameUi;


    //private SceneManager _sceneManager;

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

    public void StartPauseUi()
    {
        Time.timeScale = 0.0f;
        _pauseUi.SetActive(true);
    }

    public void StopPauseUi()
    {
        Time.timeScale = 1.0f;
        _pauseUi.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Win()
    {
        _endGameUi.SetActive(true);
        _winText.gameObject.SetActive(true);
    }

    public void Loss()
    {
        _endGameUi.SetActive(true);
        _lossText.gameObject.SetActive(true);
    }
}
