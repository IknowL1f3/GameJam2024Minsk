using System.Collections;
using UnityEngine;

public class Mimic : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    public float speed = 2.0f;
    public float rotationSpeed = 5f;
    public float detectionRange = 1.5f;

    public float attackDistance = 1f;
    public float attackReload = 0f;

    private bool isAgry = false;
    private bool isAttack = false;
    private bool isAlive = true;

    void Start()
    {
        _rb.freezeRotation = true;
    }

    void Update()
    {
        WaitingPlayer();
        if (isAlive && isAgry)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > attackDistance + 0.3)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, 0));


                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                isAttack = true;
            }
        }
            

    }

    void WaitingPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
            isAgry = true;
    }
}
