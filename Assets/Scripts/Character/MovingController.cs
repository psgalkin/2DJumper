using UnityEngine;

class MovingController : MonoBehaviour
{
    public enum MovingType { None = 0, Keyboard = 1, Accelerometer = 2 }
    
    [SerializeField] private MovingType _movingType;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public bool CanJump()
    {
        if (_rigidBody.velocity.y >= 0 && _rigidBody.velocity.y < 0.05)
        //if (_groundDetector.InTrigger == true && _rigidBody.velocity.y < 0) 
        {
            return true;

        }

        return false;
    }

    public void Jump()
    {
        if (CanJump())
        {
            _rigidBody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Force);
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _rigidBody.velocity.y);
        _rigidBody.velocity = movement;
    }

    private void FixedUpdate()
    {
        //Move();
        Jump();
    }

    //Update is called once per frame
    private void Update()
    {
        Move();
        //Jump();

    }
}
