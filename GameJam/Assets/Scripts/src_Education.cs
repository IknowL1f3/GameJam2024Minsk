using UnityEngine;

public class CylinderHandler : MonoBehaviour
{
    public Material newMaterial; // ��������, ������� ����� �������� � ���������� ��������
    public GameObject nextCylinder; // ������ �� ��������� �������
    public GameObject nextTorus;
    public GameObject Torus;
    private FadeInUI fadeInUI;

    private void Start()
    {
        fadeInUI = FindObjectOfType<FadeInUI>(); // ����� ��������� FadeInUI � �����
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ��������������, ��� � ������ ���������� ��� "Player"
        {
            // ������� ������� �������
            Destroy(gameObject);
            Destroy(Torus);

            // �������� �������� ���������� ��������
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
