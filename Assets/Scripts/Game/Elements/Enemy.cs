using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType _type;

    public EnemyType GetType() { return _type; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.Projectile))
        {
            if (collision.GetComponent<Rocket>())
            {
                collision.GetComponent<Rocket>().StartExplosion();
                Sounds.Instance.StartAttackSound(WeaponType.Explosion);
            }
            else { Destroy(collision.gameObject); }
            if (_type == EnemyType.Barrier)
            {
                Sounds.Instance.StopEnemySound(_type);
            }

            Destroy(gameObject);
        }
    }
}
