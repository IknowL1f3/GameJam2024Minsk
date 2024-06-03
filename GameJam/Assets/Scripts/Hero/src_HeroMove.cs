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
            if (Input.GetMouseButtonDown(0) && !IsAnyAttackActive())
            {
                numberOfAttack++;
                Debug.Log($"номер атаки {numberOfAttack}");
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
                        attackCorontine.Add(attackCoroutine);
                        isThirdAttackStart = true;
                    }
                    break;
                case 4:
                    numberOfAttack = 0;
                    isFirstAttackStart = false;
                    isSecondAttackStart = false;
                    isThirdAttackStart = false;
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
        attackCorontine.Clear();
    }

    public bool IsAnyAttackActive()
    {
        return attackCorontine.Count > 0;
    }

}