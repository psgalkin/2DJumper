using UnityEngine;

class EndLevelBorder : MonoBehaviour
{
    GameLogic _gameLogic;

    private void Start()
    {
        _gameLogic = FindObjectOfType<GameLogic>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Player))
        {
            _gameLogic.Win();
        }
    }
}
