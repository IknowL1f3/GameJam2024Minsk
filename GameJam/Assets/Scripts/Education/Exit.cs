using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Exit1 : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Text;
    public float fadeDuration = 1f; // Продолжительность плавного отображения
    public GameObject door; // Объект двери
    public string playerTag = "Player"; // Тег, который присвоен игроку
    public GameObject Cube;
    
    void Start()
    {
        StartCoroutine(ShowElements());
        
    }

    IEnumerator ShowElements()
    {
        CanvasGroup panelCanvasGroup = Panel.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = Text.GetComponent<CanvasGroup>();

        if (panelCanvasGroup == null) panelCanvasGroup = Panel.AddComponent<CanvasGroup>();
        if (textCanvasGroup == null) textCanvasGroup = Text.AddComponent<CanvasGroup>();

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            panelCanvasGroup.alpha = alpha;
            textCanvasGroup.alpha = alpha;

            yield return null;
        }

        panelCanvasGroup.alpha = 1f;
        textCanvasGroup.alpha = 1f;
        Cube.SetActive(true);
    }


    private void Update()
    {
 
    }
    IEnumerator FadeOutElements()
    {
        yield return new WaitForSeconds(3f);

        Debug.Log("Начало изменения");
        CanvasGroup panelCanvasGroup = Panel.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = Text.GetComponent<CanvasGroup>();

        if (panelCanvasGroup == null) panelCanvasGroup = Panel.AddComponent<CanvasGroup>();
        if (textCanvasGroup == null) textCanvasGroup = Text.AddComponent<CanvasGroup>();

        float elapsedTime = 0f;
        float startAlpha = 1f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);

            panelCanvasGroup.alpha = alpha;
            textCanvasGroup.alpha = alpha;

            yield return null;
        }
        panelCanvasGroup.alpha = 0f;
        textCanvasGroup.alpha = 0f;
        yield return new WaitForSeconds(1f);
        Debug.Log("Конец изменения");
        
    }

}
