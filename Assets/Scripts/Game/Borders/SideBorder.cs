using UnityEngine;
class SideBorder : Border
{
    private void Start()
    {
        _type = BorderType.Side;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tag.Player))
        {
            Destroy(collision.gameObject);
        }
    }
}
