using UnityEngine;

class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType _type;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.Projectile))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
