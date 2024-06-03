using System.Collections;
using System.Collections.Generic;
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

    private int numberOfAttack = 0;
    private bool isFirstAttackStart = false;
    private bool isSecondAttackStart = false;
    private bool isThirdAttackStart = false;
    private List<Coroutine> attackCorontine = new List<Coroutine>();
    private bool isNowAttack = false;


    private void Start()
    {
        _rb.freezeRotation = true;
        if (sword != null)
        {
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
        Die();
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
                    Debug.Log($"номер атаки {numberOfAttack}");
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
                        Debug.Log("Первая атака началась");
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
                        Debug.Log("Вторая атака началась");
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
                        Debug.Log("Вторая атака началась");
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
        Debug.Log("Атака закончилась");
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
            if (Input.GetMouseButtonDown(0)) // Проверка на клик ЛКМ (0 - левая кнопка мыши)
            {
                Debug.Log("ЛКМ нажата");
                isNowAttack = true;
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        numberOfAttack = 0;
        isFirstAttackStart = false;
        isSecondAttackStart = false;
        Debug.Log("Задержка завершена");
    }

    public bool IsAnyAttackActive()
    {
        return attackCorontine.Count > 0;
    }

    private void Die()
    {
       
    }

    IEnumerator DieWaitor(float delay)
    {
        yield return new WaitForSeconds(delay);
     //   Destroy(heroObject);
    }
}