using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PushAbility : MonoBehaviour
{
    public float pushForce = 10f; // Сила отталкивания
    public float pushRadius = 5f; // Радиус действия способности
    public int maxPushableObjects = 10; // Максимальное количество объектов, которые можно оттолкнуть за один раз
    public MovementHero movementHero;
    private bool isReload;
    public Image imagePush;
    public Sprite openSprite;
    public Sprite reloadSprite;
    public Text textReload;
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
                    imagePush.sprite = openSprite;
                    isReload = false;
                    return;
                }
                if (!isReload)
                {

                    hero.isPushActive = true;
                    ActivatePush();
                }
                else { }
            }
        }
    }

    void ActivatePush()
    {
        isReload = true;
        hero.GetKarma(15);
        movementHero.anim.SetBool("isPushCast", true);
        movementHero._speed = 0;
        StartCoroutine(PushAnimDelay());
        
    }

    void StartReload()
    {
        if (!isReload)
        {
            imagePush.sprite = reloadSprite;
            StartCoroutine(ReloadCountdown(15));
        }
    }

    IEnumerator ReloadCountdown(int time)
    {
        isReload = true;
        int remainingTime = time;

        while (remainingTime > 0)
        {
            textReload.text = remainingTime.ToString();
            yield return new WaitForSeconds(1);
            remainingTime--;
        }

        isReload = false;
        imagePush.sprite = openSprite;
        hero.isPushActive = false;
        textReload.text = string.Empty;
    }
    IEnumerator PushAnimDelay()
    {
        yield return new WaitForSeconds(2.3f);
        movementHero.anim.SetBool("isPushCast", false);
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
        movementHero._speed = 5;
        StartReload();
    }
}