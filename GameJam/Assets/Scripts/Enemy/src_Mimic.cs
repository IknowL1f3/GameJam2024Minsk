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

    public string targetTag;

    public int HP = 100;

    //private Hero hero = Hero.Instance();

    void Start()
    {
        _rb.freezeRotation = true;
    }

    void Update()
    {
        WaitingPlayer();
        if (isAlive && isAgry && !isAttack)
        {

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > attackDistance + 0.3)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                Vector3 direction = (player.position - transform.position).normalized;

                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        
            else
            {
                isAttack = true;
                animator.SetBool("isAttack", true);
                animator.SetBool("isWalk", false);
                StartCoroutine(StartAttackDelay());
            }
        }
    }

    IEnumerator StartAttackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (Vector3.Distance(transform.position, player.position) <= attackDistance + 0.7)
        {
            //логика атаки
            player.GetComponent<Hero>().GetDamage(20);
            HealthBar.AdjustCurrentValue(player.GetComponent<Hero>().hp);
            Debug.Log("Атакован");
        }
        else
        {
            //логика если промазал
            Debug.Log("Слишком далеко для атаки");
        }
        yield return new WaitForSeconds(1f); // Delay между атаками
        animator.SetBool("isAttack", false);
        isAttack = false;
    }

    void WaitingPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            StartCoroutine(SetAgryAfterDelay());
            animator.SetBool("isAgry", true);
        }
    }
    IEnumerator SetAgryAfterDelay()
    {
        yield return new WaitForSeconds(1);
        isAgry = true;
        animator.SetBool("isWalk", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            HP -= 50;
            if (HP <= 0)
            {
                isAlive = false;
                animator.SetBool("isDie", true);
                Karma.AdjustCurrentValue(5);
                StartCoroutine(WaitingDieing());
            }
        }

    }

    IEnumerator WaitingDieing()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
