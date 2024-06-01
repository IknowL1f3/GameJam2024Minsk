using System.Collections;
using UnityEngine;

public class MovementHero : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private Animator anim;
    
    private Vector3 _input;

    public GameObject sword; // Объект меча
    private BoxCollider swordCollider;

    private void Start()
    {
        _rb.freezeRotation = true;
        if (sword != null)
        {
            // Получаем компонент BoxCollider меча
            swordCollider = sword.GetComponent<BoxCollider>();
            swordCollider.isTrigger = false;
        }
        else
        {
            Debug.LogError("Sword object is not assigned!");
        }
    }
    private void Update()
    {
        GatherInput();
        Look();
        Attack();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private Coroutine idleCoroutine;
    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (_input != Vector3.zero)
        {
            anim.SetBool("isRun", true);
            anim.SetBool("isWait", false);

            if (idleCoroutine != null)
            {
                StopCoroutine(idleCoroutine);
                idleCoroutine = null;
            }
        }
        else
        {
            anim.SetBool("isRun", false);
            if (idleCoroutine == null)
            {
                idleCoroutine = StartCoroutine(SetIdleAfterDelay(1,3.0f));
            }
        }
    }

    IEnumerator SetIdleAfterDelay(int c, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (c==1)
        anim.SetBool("isWait", true);
        if (c == 2)
        {
            swordCollider.isTrigger = false;
            Debug.Log("Sword's BoxCollider is not a trigger.");
        }
    }


    void Look()
    {
        if (_input != Vector3.zero)
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
    void Attack()
    {
            if (swordCollider != null)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    swordCollider.isTrigger = true;
                Debug.Log("Sword's BoxCollider is now a trigger.");
                StartCoroutine(SetIdleAfterDelay(2, 1.0f));
                

            }
            
        }
        }
    
}