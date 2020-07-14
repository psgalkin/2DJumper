using UnityEngine;

class Coin : MonoBehaviour
{
    private Transform _characterTransform;
    private Character _character;
    private float _halfScreenSize;
    private Transform _cameraTransform;
    private Rigidbody2D _rigidbody;

    private float _speed;

    private void Start()
    {
        _character = FindObjectOfType<Character>();
        _characterTransform = _character.transform;
        _halfScreenSize = Camera.main.orthographicSize * 2.0f;
        _cameraTransform = Camera.main.transform;
        _rigidbody = GetComponent<Rigidbody2D>();

        _speed = _character.GetCoinSpeed();
    }

    private void Update()
    {
        if (_character.IsMagnetWorking() &&
            transform.position.y < _cameraTransform.position.y + _halfScreenSize)
        {
            _rigidbody.velocity = 
                (new Vector2(_characterTransform.position.x - transform.position.x,
                 _characterTransform.position.y - transform.position.y)).normalized * _speed;
        }
    }

}
