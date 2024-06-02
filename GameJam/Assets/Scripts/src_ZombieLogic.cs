using System.Collections;
using UnityEngine;

public class MobMovement : MonoBehaviour
{ 
    [SerializeField] private Rigidbody _rb;
    [SerializeField] public Transform player;

    public float speed = 2.0f;
    public float rotationSpeed = 5f;
    public float detectionRange = 2.0f;


    public float attackDistance = 1f;
    public float attackReload = 0f;

    private bool isAttack = false;


    
    void Start()
    {
        _rb.freezeRotation = true;
    }

    void Update()
    {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange && !isAttack)
            {
                if (distanceToPlayer > attackDistance + 0.7)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                    Vector3 direction = (player.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                }
                else
                {
                    isAttack = true;
                    StartCoroutine(StartAttackDelay());
                }
            }
    }

    IEnumerator StartAttackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(transform.position, player.position) <= attackDistance + 0.7)
        {
            Debug.Log("Атакован");
            HealthBar.AdjustCurrentValue(-10);
        }
        isAttack = false;
    }

}   
      
