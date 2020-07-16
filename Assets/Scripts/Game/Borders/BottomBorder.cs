using UnityEngine;
class BottomBorder : Border
{
    private void Start()
    {
        _type = BorderType.Bottom;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tag.Player))
        {
            Destroy(collision.gameObject);
        }
    }
}
