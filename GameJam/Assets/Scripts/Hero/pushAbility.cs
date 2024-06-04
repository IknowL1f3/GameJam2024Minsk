using System.Collections.Generic;
using UnityEngine;

public class PushAbility : MonoBehaviour
{
    public float pushForce = 10f; // ���� ������������
    public float pushRadius = 5f; // ������ �������� �����������
    public int maxPushableObjects = 10; // ������������ ���������� ��������, ������� ����� ���������� �� ���� ���

    private Hero hero;

    private void Start()
    {
        hero = Hero.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hero.GetKarma(15);
            ActivatePush();
        }
    }

    void ActivatePush()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius);
        int pushedCount = 0;

        foreach (Collider hitCollider in hitColliders)
        {
            if (pushedCount >= maxPushableObjects) break;
            Mimic mimic = hitCollider.GetComponent<Mimic>();
            MobMovement mob = hitCollider.GetComponent<MobMovement>();

            if (mimic != null || mob != null)
            {
                Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    hero.GetKarma(-1);
                    Vector3 pushDirection = hitCollider.transform.position - transform.position;
                    rb.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
                    pushedCount++;
                }
            }
        }
    }
}