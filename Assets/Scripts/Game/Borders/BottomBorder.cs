using UnityEngine;
class BottomBorder : Border
{
    private void Start()
    {
        _type = BorderType.Bottom;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Enemy))
        {
            if (collision.GetComponent<Enemy>().GetType() == EnemyType.Barrier) {
                Sounds.Instance.StopEnemySound(EnemyType.Barrier);
            }
            else if (collision.GetComponent<Enemy>().GetType() == EnemyType.Pusher) {
                Sounds.Instance.StopEnemySound(EnemyType.Pusher);
            }
        }
        if (!collision.CompareTag(Tag.Player))
        {
            Destroy(collision.gameObject);
        }
        
    }
}
