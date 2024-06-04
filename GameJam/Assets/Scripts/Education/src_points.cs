using UnityEngine;

public class CylinderHandler : MonoBehaviour
{
    public Material newMaterial; // Материал, который будет применен к следующему цилиндру
    public GameObject nextCylinder; // Ссылка на следующий цилиндр
    public GameObject nextTorus;
    public GameObject Torus;
    public MarkerCollector markerCollector; // Ссылка на скрипт отслеживания сбора меток
    private FadeInWASD fadeInUI;

    private void Start()
    {
        fadeInUI = FindObjectOfType<FadeInWASD>(); // Найти компонент FadeInUI в сцене
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Destroy(gameObject);
            Destroy(Torus);

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

            if (markerCollector != null)
            {
                MarkerCollector.AddMarker();
                Debug.Log("маркер добавлен");
            }
        }
    }
}
