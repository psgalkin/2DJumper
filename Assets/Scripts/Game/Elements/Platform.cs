using UnityEngine;
class Platform : MonoBehaviour
{
    [SerializeField] private PlatformType _type;

    public PlatformType GetType()
    {
        return _type;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int i = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int i = 4;
    }
}
