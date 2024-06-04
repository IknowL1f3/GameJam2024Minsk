using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    public float speed = 2.0f;
    public float rotationSpeed = 5f;
    public float detectionRange = 2.0f;

    public string targetTag;

    public float attackDistance = 1f;
    public float attackReload = 0f;

    private bool isAttack = false;
    private bool isAlive = true;

    public int HP = 100;

    private Hero hero;


    // Метод, вызываемый при входе другого коheroллайдера в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            HP -= 40;
            if (HP <= 0)
            {
                isAlive = false;
                animator.SetBool("isDie", true);
                hero.GetKarma(-5);
                hero.GetSoul(1);
                StartCoroutine(WaitingDieing());
            }
        }

    }

    IEnumerator WaitingDieing()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    void Start()
    {
        hero = Hero.Instance;
        _rb.freezeRotation = true;
    }

    void Update()
    {
        if (isAlive)
            WalkAttackZombie();

    }

    void WalkAttackZombie()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !isAttack)
        {
            if (distanceToPlayer > attackDistance + 0.3)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                animator.SetBool("isWalk", true);
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
        yield return new WaitForSeconds(1.4f);
        if (Vector3.Distance(transform.position, player.position) <= attackDistance + 0.7)
        {
            hero.GetDamage(10);
            
        }
        else
        {
            //логика если промазал
        }
        animator.SetBool("isAttack", false);
        StartCoroutine(AttackEndDelay());

    }

    IEnumerator AttackEndDelay()
    {
        yield return new WaitForSeconds(1.26f);
        isAttack = false;
    }

    public void GetHit(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            isAlive = false;
            animator.SetBool("isDie", true);
            Karma.AdjustCurrentValue(5);
            hero.GetSoul(1);
            StartCoroutine(WaitingDieing());
        }
    }
}

