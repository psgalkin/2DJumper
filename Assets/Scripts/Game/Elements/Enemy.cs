using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType _type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.Projectile))
        {
            if (collision.GetComponent<Rocket>())
            {
                collision.GetComponent<Rocket>().StartExplosion();
            }
            else { Destroy(collision.gameObject); }
            Destroy(gameObject);
        }
    }
}
