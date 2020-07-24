using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private InGameUi _ui;

    private void Start()
    {
        _ui = FindObjectOfType<InGameUi>();
        Time.timeScale = 1.0f;
    }



    public void Win()
    {
        _ui.Win();
        Time.timeScale = 0;
    }

    public void Loss()
    {
        _ui.Loss();
        Time.timeScale = 0;
    }
}
