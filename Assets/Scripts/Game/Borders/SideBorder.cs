using UnityEngine;
class SideBorder : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Projectile))
        {
            Destroy(collision.gameObject);
        }
    }
}
