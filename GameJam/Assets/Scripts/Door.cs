using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Door : MonoBehaviour
{
    public GameObject DoorPrefab;
    public string playerTag = "Player";
    public CanvasGroup panelCanvasGroup; // Ссылка на CanvasGroup панели
    public float fadeDuration = 1.0f;    // Продолжительность появления панели

    void Start()
    {
        // Убедитесь, что панель невидима в начале
        if (panelCanvasGroup != null)
        {
            panelCanvasGroup.alpha = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Анимация");
            if (panelCanvasGroup != null)
            {
                StartCoroutine(FadeInPanel());
            }
        }
    }

    private IEnumerator FadeInPanel()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            panelCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        panelCanvasGroup.alpha = 1f;
    }
}
