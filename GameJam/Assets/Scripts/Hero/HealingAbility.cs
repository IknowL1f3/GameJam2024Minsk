using System.Collections;
using UnityEngine;

public class HealingAbility : MonoBehaviour
{
    private bool isAbilityReady = true;
    public Hero hero;
    public GameObject healingParticlesPrefab; // Префаб частиц
    private ParticleSystem healingParticlesInstance; // Экземпляр ParticleSystem
    public Rigidbody rb;
    public MovementHero movementHero;
    private bool isReload = false;

    void Start()
    {

         hero = Hero.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (hero.hp > 0)
            {
                if (!hero.heal)
                {
                    AbilityShop shop = new AbilityShop();
                    shop.BuyHeal();
                    //открытие способности
                    return;
                }
                if (!isReload)
                    ActivateHealingAbility();
            }
        }
    }
    void StartReload()
    {
        if (!isReload)
        {
            StartCoroutine(ReloadCountdown(10));
        }
    }

    IEnumerator ReloadCountdown(int time)
    {
        isReload = true;
        int remainingTime = time;

        while (remainingTime > 0)
        {
            Debug.Log(remainingTime);
            yield return new WaitForSeconds(1);
            remainingTime--;
        }

        isReload = false;
        Debug.Log("Reload complete");
    }

    public void ActivateHealingAbility()
    {
        if (isAbilityReady && hero._heal)
        {
            if (healingParticlesPrefab != null)
            {
                if (healingParticlesInstance == null)
                {
                    GameObject healingParticlesObject = Instantiate(healingParticlesPrefab, rb.transform.position, Quaternion.Euler(-90, 0, 0));
                    healingParticlesInstance = healingParticlesObject.GetComponent<ParticleSystem>();
                    healingParticlesObject.transform.parent = rb.transform;
                }
                healingParticlesInstance.transform.position = rb.transform.position;
                healingParticlesInstance.transform.rotation = Quaternion.Euler(-90, 0, 0); // Установка поворота по X на -90 градусов
                healingParticlesInstance.Play();
            }
            StartCoroutine(HealAfterDelay());
            isAbilityReady = false;
        }
    }

    IEnumerator HealAfterDelay()
    {
        hero.GetKarma(5);
        movementHero.anim.SetBool("isFireballOrHeal", true);
        StartCoroutine(HealAnimDelay());
        yield return new WaitForSeconds(2.5f);
        if (healingParticlesInstance != null)
        {
            healingParticlesInstance.Stop();
        }
        hero.Heal(15);
        Debug.Log(hero.hp);
        isAbilityReady = true;
        StartReload();
    }
    IEnumerator HealAnimDelay()
    {
        yield return new WaitForSeconds(1f);
        movementHero.anim.SetBool("isFireballOrHeal", false);
    }
}
