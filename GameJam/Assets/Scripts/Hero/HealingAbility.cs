using System.Collections;
using UnityEngine;

public class HealingAbility : MonoBehaviour
{
    private bool isAbilityReady = true;
    public Hero hero;
    public GameObject healingParticlesPrefab; // Префаб частиц
    private ParticleSystem healingParticlesInstance; // Экземпляр ParticleSystem
    public Rigidbody rb;

    void Start()
    {

         hero = Hero.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            hero.GetKarma(5);
            ActivateHealingAbility();
        }
    }

    public void ActivateHealingAbility()
    {
        if (isAbilityReady)
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
        yield return new WaitForSeconds(2.5f);

        if (healingParticlesInstance != null)
        {
            healingParticlesInstance.Stop();
        }
        hero.Heal(15);
        Debug.Log(hero.hp);
        isAbilityReady = true;
    }
}
