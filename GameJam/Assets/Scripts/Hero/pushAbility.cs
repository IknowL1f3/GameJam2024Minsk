using System.Collections.Generic;
using UnityEngine;

public class PushAbility : MonoBehaviour
{
    public float pushForce = 10f; // Сила отталкивания
    public float pushRadius = 5f; // Радиус действия способности
    public int maxPushableObjects = 10; // Максимальное количество объектов, которые можно оттолкнуть за один раз

    private Hero hero;

    private void Start()
    {
        hero = Hero.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (hero.hp > 0)
            {
                if (!hero.push)
                {

                    AbilityShop shop = new AbilityShop();
                    shop.BuyPush();
                    //открытие способности
                    return;
                }
                ActivatePush();
            }
        }
    }

    void ActivatePush()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pushRadius);
        int pushedCount = 0;
        hero.GetKarma(15);
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