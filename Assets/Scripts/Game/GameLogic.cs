using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    void Start()
    {
        
    }

    public void Win()
    {
        Time.timeScale = 0;

    }

    public void Loss()
    {
        Time.timeScale = 0;
    }
}
