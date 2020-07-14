using UnityEngine;
class UpperBorder : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.Projectile))
        {
            Destroy(collision.gameObject);
        }
    }
}
