using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    public GameObject fireballPrefab; // Префаб фаерболла
    public float fireballSpeed = 10f; // Скорость фаерболла
    public Vector3 fireballOffset = new Vector3(0, 1, 1); // Смещение позиции фаерболла относительно персонажа
    public float fireballLifetime = 3f; // Время жизни фаерболла

    private Hero hero;

    private void Start()
    {
        hero = Hero.Instance;
    }

    void Update()
    {
        // Запуск фаерболла при нажатии на клавишу (например, пробел)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchFireball();
        }
    }

    void LaunchFireball()
    {
        hero.GetKarma(10);
        // Вычисление позиции запуска фаерболла с учетом смещения относительно персонажа
        Vector3 fireballPosition = transform.position + transform.TransformDirection(fireballOffset);

        // Создание фаерболла из префаба в вычисленной позиции
        GameObject fireball = Instantiate(fireballPrefab, fireballPosition, transform.rotation);

        // Отключение гравитации для фаерболла
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }

        // Добавление скрипта для движения фаерболла
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
        // Движение фаерболла вперед
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Уменьшение времени жизни
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверка на класс `MobMovement`
        MobMovement mob = other.GetComponent<MobMovement>();
        if (mob != null)
        {
            mob.GetHit(50);
            Destroy(gameObject);
            return;
        }

        // Проверка на класс `Mimic`
        Mimic mimic = other.GetComponent<Mimic>();
        if (mimic != null)
        {
            mimic.GetHit(20);
            Destroy(gameObject);
            return;
        }

        // Если коллайдер не принадлежит ни `MobMovement`, ни `Mimic`, уничтожаем фаерболл
        Destroy(gameObject);
    }
}
