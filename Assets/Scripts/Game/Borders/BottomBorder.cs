using UnityEngine;
class BottomBorder : MonoBehaviour
{
    private GameLogic _gameLogic;

    private void Start()
    {
        _gameLogic = FindObjectOfType<GameLogic>();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Player))
        {
            _gameLogic.Loss();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        

    //}
}
