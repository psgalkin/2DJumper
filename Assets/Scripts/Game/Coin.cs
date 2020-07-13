using UnityEngine;

class Coin : MonoBehaviour
{
    private Transform _characterTransform;

    private void Start()
    {
        _characterTransform = FindObjectOfType<Character>().transform;
    }

}
