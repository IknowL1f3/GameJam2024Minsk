using UnityEngine;

public class MovementHero : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;

    private Vector3 _input;

    private void Start()
    {
        _rb.freezeRotation = true;
    }
    private void Update()
    {
        GatherInput();
        Look();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        if(_input !=Vector3.zero)
        {

            var relative = (transform.position + _input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }
        
    }

    void Move()
    {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
    }
}