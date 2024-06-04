using UnityEngine;

public class CylinderHandler : MonoBehaviour
{
    public Material newMaterial; // Материал, который будет применен к следующему цилиндру
    public GameObject nextCylinder; // Ссылка на следующий цилиндр
    public GameObject nextTorus;
    public GameObject Torus;
    private FadeInUI fadeInUI;

    private void Start()
    {
        fadeInUI = FindObjectOfType<FadeInUI>(); // Найти компонент FadeInUI в сцене
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Предполагается, что у игрока установлен тег "Player"
        {
            // Удаляем текущий цилиндр
            Destroy(gameObject);
            Destroy(Torus);

            // Изменяем материал следующего цилиндра
            if (nextCylinder != null)
            {
                Renderer renderer = nextCylinder.GetComponent<Renderer>();
                CapsuleCollider capsuleCollider = nextCylinder.GetComponent<CapsuleCollider>();
                if (renderer != null && newMaterial != null)
                {
                    capsuleCollider.isTrigger = true;
                    renderer.material = newMaterial;
                    nextTorus.SetActive(true);
                }
            }
            if (fadeInUI != null)
            {
                fadeInUI.IncrementCylinderCounter();
            }
        }
    }
}
