using System.Numerics;
using UnityEngine;
using TMPro;

using Vector2 = UnityEngine.Vector2;

class MovingController : MonoBehaviour
{
    public enum MovingType { None = 0, Keyboard = 1, Gyroscope = 2 }
    
    [SerializeField] private MovingType _movingType;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;
    
    private Rigidbody2D _rigidBody;

    private bool _isForceJump = false;
    private bool _isRandomJump = false;
    private bool _isFlying = false;
    private float _jumpVelocityX = 0.0f;

    private PlatformType _currentPlatform;

    private float _forceCoef;
    private float _flyingSpeed;

    private Gyroscope _gyro;

    private void Start()
    {
        _gyro = Input.gyro;
        _gyro.enabled = true;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public bool CanJump()
    {
        if (_rigidBody.velocity.y >= 0.0f && _rigidBody.velocity.y < 0.01f)
        {
            return true;
        }

        return false;
    }

    public void SetForceCoef(float coef)
    {
        _forceCoef = coef;
    }

    public void SetFlyingSpeed(float speed)
    {
        _flyingSpeed = speed;
    }

    public void StopJump()
    {
        _isForceJump = false;
        _isRandomJump = false;
    }

    public void ForceJump()
    {
        _isForceJump = true;
    }

    public void RandomJump()
    {
        _isRandomJump = true;
    }

    public void StartFlying()
    {
        _isFlying = true;
    }

    public void StopFlying()
    {
        _isFlying = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Platform))
        {
            _currentPlatform = collision.GetComponent<Platform>().GetType();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tag.Platform))
        {
            _currentPlatform = collision.gameObject.GetComponent<Platform>().GetType();
        }
    }

    public void Jump()
    {
        if (CanJump())
        {
            if (_isForceJump)
            {
                Sounds.Instance.StartBoostSound(BoostType.Trampoline);
                _rigidBody.AddForce(new Vector2(0, _jumpForce * _forceCoef), ForceMode2D.Force);
                _isForceJump = false; 
            }
            else if (_isRandomJump)
            {
                Sounds.Instance.StartJumpSound(PlatformType.Trap);
                Vector2 forceVect = new Vector2(Random.Range(-7f, 7f), 1.0f).normalized * _jumpForce;
                _rigidBody.AddForce(forceVect * 0.005f, ForceMode2D.Force);
                _jumpVelocityX = forceVect.x;
                _isRandomJump = false;
            }
            else
            {
                _rigidBody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Force);
            }
            Sounds.Instance.StartJumpSound(_currentPlatform);
        }
    }

    private void Fly()
    {
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _flyingSpeed);
    }

    private void Move()
    {
        if (_movingType == MovingType.Keyboard)
        {
            float deltaX = Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime;
            Vector2 movement = new Vector2(deltaX, _rigidBody.velocity.y);
            if (Mathf.Abs(_jumpVelocityX) > 0.15f)
            {
                Debug.Log(_jumpVelocityX);
                movement = new Vector2(deltaX + _jumpVelocityX * 0.01f, _rigidBody.velocity.y);
                _jumpVelocityX += (_jumpVelocityX > 0) ? -5f : 5f;
            }

            _rigidBody.velocity = movement;
        }
        else if (_movingType == MovingType.Gyroscope)
        {
            float deltaX = GyroToUnity(_gyro.attitude).x * 2f * _rotationSpeed * Time.deltaTime;

            //_debugText.text = $"x:{gyro.x}, y:{gyro.y}, z:{gyro.z}, w:{gyro.eulerAngles.x}/n angle: x:{gyro.eulerAngles.x}, y:{gyro.eulerAngles.x}";
            Vector2 movement = new Vector2(deltaX, _rigidBody.velocity.y);
            if (Mathf.Abs(_jumpVelocityX) > 0.15f)
            {
                Debug.Log(_jumpVelocityX);
                movement = new Vector2(deltaX + _jumpVelocityX * 0.01f, _rigidBody.velocity.y);
                _jumpVelocityX += (_jumpVelocityX > 0) ? -5f : 5f;
            }

            _rigidBody.velocity = movement;
        }
    }

    private UnityEngine.Quaternion GyroToUnity(UnityEngine.Quaternion q)
    {
        return new UnityEngine.Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private void FixedUpdate()
    {
        if (_isFlying) { Fly(); }
        else { Jump(); }
    }

    //Update is called once per frame
    private void Update()
    {
        Move();
        //Jump();
    }
}
