using UnityEngine;

class BorderSwitcher : MonoBehaviour
{
    private float _lvlWidth;

    private void Start()
    {
        _lvlWidth = Camera.main.orthographicSize * 2.0f * Camera.main.aspect;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Border) &&
            collision.GetComponent<Border>().GetType() == BorderType.Side)
        {
            if (transform.position.x <= 0)
            {
                GetComponentInParent<Character>().transform.position =
                    new Vector3(transform.position.x + _lvlWidth, transform.position.y, transform.position.z);
            }
            else
            {
                GetComponentInParent<Character>().transform.position =
                    new Vector3(transform.position.x - _lvlWidth, transform.position.y, transform.position.z);
            }
        }
    }

    private void OnTriggerStay(Collider2D collision)
    {
        //if (collision.CompareTag(Tag.Border) &&
        //    collision.GetComponent<Border>().GetType() == BorderType.Side)
        //{
        //    if (transform.position.x <= 0)
        //    {
        //        GetComponentInParent<Character>().transform.position =
        //            new Vector3(transform.position.x + _lvlWidth, transform.position.y, transform.position.z);
        //    }
        //    else
        //    {
        //        GetComponentInParent<Character>().transform.position =
        //            new Vector3(transform.position.x - _lvlWidth, transform.position.y, transform.position.z);
        //    }
        //}
    }
}
