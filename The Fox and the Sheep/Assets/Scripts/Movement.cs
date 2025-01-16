using UnityEngine;

public class Movement : MonoBehaviour

{
    // Player properties
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isSheep;

    // Movement properties
    private Vector2 _movement;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashSpeed = 8f;
    private bool _isDashing;
    private bool _isOutside;

    // Circle properties
    [SerializeField] private Vector2 center = new(0, 0); // Fixed point of rotation (e.g., origin)
    [SerializeField] private float radius = 4f;
    private float _angle;

    private void Start()
    {
        _isDashing = false;
    }

    // Update is called once per frame
    private void Update()
    {
        ReadInput();
        MapToCircle();

        ChangePens();
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + _movement * (speed * Time.fixedDeltaTime));
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
                _isDashing = true;
                _isOutside = !_isOutside;
            }
        }
        else
        {
            _movement.x = Input.GetAxisRaw("HorizontalARROWS");
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isDashing = true;
                _isOutside = !_isOutside;
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
        var x = center.x + Mathf.Cos(_angle) * radius;
        var y = center.y + Mathf.Sin(_angle) * radius;

        // Set the object's position to the new calculated position
        rb.position = new Vector2(x, y);
    }

    private void ChangePens()
    {
        SetRadius();
    }

    private void SetRadius()
    {
        radius = _isOutside ? 2f : 4f; // Alternate between inner and outer circle 
    }

    private void Dash()
    {
    }
}