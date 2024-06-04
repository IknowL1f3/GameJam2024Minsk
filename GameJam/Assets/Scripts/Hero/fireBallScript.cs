using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 10f;
    public Vector3 fireballOffset = new Vector3(0, 1, 2); 
    public float fireballLifetime = 3f;


    private Hero hero;

    private void Start()
    {
        hero = Hero.Instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hero.hp > 0)
            {
                if (!hero.fireball)
                {
                    AbilityShop shop = new AbilityShop();
                    shop.BuyFireBall();
                    //открытие способности
                    return;
                }
                LaunchFireball();
            }
        }
    }

    void LaunchFireball()
    {
        hero.GetKarma(10);
        Vector3 fireballPosition = transform.position + transform.TransformDirection(fireballOffset);
        GameObject fireball = Instantiate(fireballPrefab, fireballPosition, transform.rotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
        FireballMovement movement = fireball.AddComponent<FireballMovement>();
        movement.SetSpeed(fireballSpeed);
        movement.SetLifetime(fireballLifetime);
    }
}

public class FireballMovement : MonoBehaviour
{
    private float speed;
    private float lifetime;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetLifetime(float newLifetime)
    {
        lifetime = newLifetime;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MobMovement mob = other.GetComponent<MobMovement>();
        if (mob != null)
        {
            mob.GetHit(50);
            Destroy(gameObject);
            return;
        }
        Mimic mimic = other.GetComponent<Mimic>();
        if (mimic != null)
        {
            mimic.GetHit(20);
            Destroy(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}
