using UnityEngine;
public class Boost : MonoBehaviour
{
    [SerializeField] private BoostType _type;

    public BoostType GetType()
    {
        return _type;
    }
}
