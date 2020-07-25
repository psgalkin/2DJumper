using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private InGameUi _ui;
    [SerializeField] private CharacterData _data;


    private void Start()
    {
        _ui = FindObjectOfType<InGameUi>();
        Time.timeScale = 1.0f;
    }

    public void Win(int coinsEarned)
    {
        _ui.Win();
        _ui.EndGameCoins(coinsEarned);
        _data.CoinCount += coinsEarned;
        Time.timeScale = 0;
    }

    public void Loss()
    {
        _ui.Loss();
        Time.timeScale = 0;
    }
}
