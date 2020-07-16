using UnityEngine;
class MovingPlatform : Platform
{
    private float _lvlWidth;
    private float _halfPlatformWidth;
    private Rigidbody2D _rigidbody;
    private float _speed;
    //private bool _movingRight;

    private void Start()
    {
        _lvlWidth = Camera.main.orthographicSize * 2.0f * Camera.main.aspect;
        _halfPlatformWidth = GetComponent<BoxCollider2D>().size.x / 2 * transform.localScale.x;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
        if (!_rigidbody) { _rigidbody = GetComponent<Rigidbody2D>(); }
        _rigidbody.velocity = Vector2.right * _speed;
    }

    private void Update()
    {

        if (_rigidbody.transform.position.x < _halfPlatformWidth + 0.2f)
        {
            _rigidbody.velocity = Vector2.right * _speed;
        }
        else if (_rigidbody.transform.position.x > _lvlWidth - _halfPlatformWidth - 0.2f)
        {
            _rigidbody.velocity = Vector2.left * _speed;
        }
    }
}
