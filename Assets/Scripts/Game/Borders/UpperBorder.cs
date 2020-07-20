using UnityEngine;
class UpperBorder : Border
{
    private void Start()
    {
        _type = BorderType.Upper;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Projectile))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag(Tag.Enemy))
        {
            TargetManager.AddEnemyToTarget(collision.GetComponent<Enemy>());
            if (collision.GetComponent<PusherEnemy>())
            {
                collision.GetComponent<PusherEnemy>().SetActive();
            }
        }
    }
}
