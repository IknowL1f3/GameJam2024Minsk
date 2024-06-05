using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class HealingAbility : MonoBehaviour
{
    private bool isAbilityReady = true;
    public Hero hero;
    public GameObject healingParticlesPrefab; // Префаб частиц
    private ParticleSystem healingParticlesInstance; // Экземпляр ParticleSystem
    public Rigidbody rb;
    public MovementHero movementHero;
    private bool isReload = false;
    public Image imageHeal;
    public Sprite openSprite;
    public Sprite reloadSprite;
    public Text textReload;

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
                    imageHeal.sprite = openSprite;
                    return;
                }
                if (!isReload )
                    ActivateHealingAbility();
                else
                {

                }
            }
        }
    }
    void StartReload()
    {
        if (!isReload)
        {
            imageHeal.sprite = reloadSprite;
            StartCoroutine(ReloadCountdown(10));
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
        imageHeal.sprite = openSprite;
        textReload.text = string.Empty;
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
        hero.Heal(50);
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
