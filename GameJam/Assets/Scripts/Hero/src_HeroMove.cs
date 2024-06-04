using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MovementHero : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private Animator anim;

    private Vector3 _input;

    public GameObject sword;
    private BoxCollider swordCollider;

    public int numberOfAttack = 0;
    private bool isFirstAttackStart = false;
    private bool isSecondAttackStart = false;
    private bool isThirdAttackStart = false;
    private List<Coroutine> attackCorontine = new List<Coroutine>();
    private bool isNowAttack = false;
    private bool isDie = false;

    private Hero hero;


    private void Start()
    {
        hero = Hero.Instance;
        _rb.freezeRotation = true;
        if (sword != null)
        {
            swordCollider = sword.GetComponent<BoxCollider>();
            swordCollider.isTrigger = false;
        }
    }
    private void Update()
    {
        Die();
        if (!isDie)
        {
            GatherInput();
            Look();
            Attack();
            InfluenceKarma();
        }
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
                idleCoroutine = StartCoroutine(SetIdleAfterDelay(3.0f));
            }
        }
    }

    IEnumerator SetIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("isWait", true);
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
            if (!IsAnyAttackActive())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    numberOfAttack = numberOfAttack == 0 ? numberOfAttack + 1 : numberOfAttack;
                }
                if (isNowAttack)
                {
                    isNowAttack = false;
                    numberOfAttack++;
                }
            }
            switch (numberOfAttack)
            {
                case 1:
                    if (!isFirstAttackStart)
                    {
                        anim.SetBool("isFirstAttack", true);
                        swordCollider.enabled = true;
                        swordCollider.isTrigger = true;
                        Coroutine attackCoroutine = StartCoroutine(AttakTimer(1.5f, "isFirstAttack"));
                        _speed = 2;
                        attackCorontine.Add(attackCoroutine);
                        isFirstAttackStart = true;
                    }
                    break;
                case 2:
                    if (!isSecondAttackStart)
                    {
                        anim.SetBool("isSecondAttack", true);
                        swordCollider.enabled = true;
                        swordCollider.isTrigger = true;
                        Coroutine attackCoroutine = StartCoroutine(AttakTimer(0.47f, "isSecondAttack"));
                        _speed = 2;
                        attackCorontine.Add(attackCoroutine);
                        isSecondAttackStart = true;
                    }
                    break;
                case 3:
                    if (!isThirdAttackStart)
                    {
                        anim.SetBool("isThirdAttack", true);
                        swordCollider.enabled = true;
                        swordCollider.isTrigger = true;
                        Coroutine attackCoroutine = StartCoroutine(AttakTimer(0.47f, "isThirdAttack"));
                        _speed = 2;
                        attackCorontine.Add(attackCoroutine);
                        isThirdAttackStart = true;
                        StartCoroutine(ReloadWaitor(0.48f));
                    }
                    break;
            }

        }
    }

    IEnumerator AttakTimer(float delay, string typeAttack)
    {
        yield return new WaitForSeconds(delay);
        swordCollider.isTrigger = false;
        swordCollider.enabled = false;
        anim.SetBool(typeAttack, false);
        _speed = 5;
        StartCoroutine(WaitAttackCombo(0.75f));
        attackCorontine.Clear();
    }

    IEnumerator ReloadWaitor(float delay)
    {
        yield return new WaitForSeconds(delay);
        numberOfAttack = 0;
        isFirstAttackStart = false;
        isSecondAttackStart = false;
        isThirdAttackStart = false;
    }

    IEnumerator WaitAttackCombo(float delay)
    {
        float timer = 0f;

        while (timer < delay)
        {
            if (Input.GetMouseButtonDown(0)) // �������� �� ���� ��� (0 - ����� ������ ����)
            {
                isNowAttack = true;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        numberOfAttack = 0;
        isFirstAttackStart = false;
        isSecondAttackStart = false;
    }

    public bool IsAnyAttackActive()
    {
        return attackCorontine.Count > 0;
    }

    private void Die()
    {
        if (hero.hp < 1)
        {
            anim.SetBool("isDie", true);
            isDie = true;
            StartCoroutine(DieWaitor(2));
            _input = Vector3.zero;
        }
    }

    IEnumerator DieWaitor(float delay)
    {
        yield return new WaitForSeconds(delay);
        //���������� ������
    }

    void InfluenceKarma()
    {
        float speed = 5;
        float animationSpeed = 1f;
        float newAnimSpeed;
        
        if(hero._karma > 50)
        {
            int karmaCount = hero._karma - 50;
            _speed = speed * (1 - (float)karmaCount / 50);
            newAnimSpeed = animationSpeed * (1 - (float)karmaCount / 50);
            anim.SetFloat("speed", newAnimSpeed);
        }
        else
        {
            anim.SetFloat("speed", 1);
        }
        if(hero._karma >= 100)
            hero._hp = 0;

    }
}