using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private InGameUi _ui;

    //private void Start()
    //{
    //    _ui = FindObjectOfType<InGameUi>();
    //}

    public void Win()
    {
        _ui.SetWinText();
        Time.timeScale = 0;
    }

    public void Loss()
    {
        _ui.SetLossText();
        Time.timeScale = 0;
    }
}
