using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour

{
    // Player properties
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isSheep;

    // Movement properties
    private Vector2 _movement;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashSpeed = 0.05f;
    private bool _isDashing;
    private bool _isOutside;
    
    // Circle properties
    [SerializeField] private Vector2 center = new(0, 0); // Fixed point of rotation (e.g., origin)
    [SerializeField] private float currentRadius;
    [SerializeField] private float innerRadius = 2f;
    [SerializeField] private float outerRadius = 4f;
    private float _angle;

    // Start is called once before the beginning of the game 
    private void Start()
    {
        // Set the _isOutside variable to be the opposite of what it is for when the toggling happens 
        _isOutside = isSheep;

        // Calculate initial angle based on the current position of the sprite relative to the center
        Vector2 direction = rb.position - center;
        currentRadius = direction.magnitude; // Use the actual distance from the center
        _angle = Mathf.Atan2(direction.y, direction.x); // Set angle based on current position
    }

    // Update is called once per frame
    private void Update()
    {
        ReadInput();
        MapToCircle();
        DashHandler(_isOutside);
    }

    private void FixedUpdate()
    {
        // Movement
        if (!_isDashing)
        {
            rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
        }
    }

    /**
     * Takes the input from the players and updates movement accordingly
     */
    private void ReadInput()
    {
        // Move the appropriate character
        if (isSheep)
        {
            _movement.x = Input.GetAxisRaw("HorizontalWASD");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isOutside = !_isOutside;
                _isDashing = true;
            }
        }
        else
        {
            _movement.x = Input.GetAxisRaw("HorizontalARROWS");
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isOutside = !_isOutside;
                _isDashing = true;
            }
        }
    }

    /**
     * Calculates and maps movement to a circle
     */
    private void MapToCircle()
    {
        // Increment angle
        _angle += _movement.x * speed * Time.deltaTime;

        // Calculate the new position relative to the fixed center point
        var x = center.x + Mathf.Cos(_angle) * currentRadius;
        var y = center.y + Mathf.Sin(_angle) * currentRadius;

        // Set the object's position to the new calculated position
        rb.position = new Vector2(x, y);
    }

    /**
     * Translates the player to the other circle (inner or outer) depending on which one it is in currently
     */
    private void DashHandler(bool isOutside)
    {
        if (isOutside && currentRadius > innerRadius && _isDashing)
        {
            currentRadius -= dashSpeed;
        }
        else if (!isOutside && currentRadius < outerRadius && _isDashing)
        {
            currentRadius += dashSpeed;
        }
        else
        {
            _isDashing = false;
        }
    }
}