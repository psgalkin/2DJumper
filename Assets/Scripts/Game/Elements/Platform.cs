using UnityEngine;

class Platform : MonoBehaviour
{
    [SerializeField] private PlatformType _type;

    public PlatformType GetType() { return _type; }
}

