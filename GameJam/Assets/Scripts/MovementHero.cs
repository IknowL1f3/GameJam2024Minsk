using UnityEngine;

public class MovementHero : MonoBehaviour
{
    public float maxMoveSpeed = 5f; // Maximum movement speed
    public float acceleration = 10f; // Acceleration
    public float deceleration = 10f; // Deceleration
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 velocity = Vector3.zero; // Declaring velocity variable

    // Key bindings
    public KeyBinding keyBindings = new KeyBinding();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Lock rotation
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        movement = Vector3.zero;

        if (Input.GetKey(keyBindings.forwardKey))
        {
            movement += transform.forward;
        }
        if (Input.GetKey(keyBindings.backwardKey))
        {
            movement += -transform.forward;
        }
        if (Input.GetKey(keyBindings.leftKey))
        {
            movement += -transform.right;
        }
        if (Input.GetKey(keyBindings.rightKey))
        {
            movement += transform.right;
        }

        movement = movement.normalized * maxMoveSpeed;
    }

    void Move()
    {
        if (movement != Vector3.zero)
        {
            velocity = Vector3.Lerp(velocity, movement, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
