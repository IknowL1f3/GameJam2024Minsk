using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoulAndHealing : MonoBehaviour
{
    public GameObject HPPanel;
    public GameObject HPText;
    public GameObject Player; // Объект игрока
    public Image imageComponent; // Компонент Image, текстура которого будет изменена
    public Sprite newSprite; // Новая текстура
    public float fadeDuration = 1f; // Продолжительность плавного отображения
    public int targetHealth = 10; // Целевое здоровье
    private bool mozno;
    private bool FHP;
    private Text HPTextComponent;
    private Hero hero;
    public GameObject Push;
    void Start()
    {
        HPTextComponent = HPText.GetComponent<Text>();
        hero = Hero.Instance;
        StartCoroutine(ShowHPElements());
    }

    IEnumerator ShowHPElements()
    {
        CanvasGroup panelCanvasGroup = HPPanel.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = HPText.GetComponent<CanvasGroup>();

        if (panelCanvasGroup == null) panelCanvasGroup = HPPanel.AddComponent<CanvasGroup>();
        if (textCanvasGroup == null) textCanvasGroup = HPText.AddComponent<CanvasGroup>();

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

        GiveSoulAndUnlockHealing();
    }

    void GiveSoulAndUnlockHealing()
    {
        mozno = true;
        
        hero._hp = 10;
        hero._souls = 50;
    }
    private void Update()
    {
        if(mozno && Input.GetKeyDown(KeyCode.G))
        {
            imageComponent.sprite = newSprite;
            mozno = false;
            FHP = true;
        }
        if(hero._hp ==100 && FHP)
        {
            StartCoroutine(FadeOutElements());
            FHP = false;
        }
    }
    IEnumerator FadeOutElements()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Начало изменения");
        CanvasGroup panelCanvasGroup = HPPanel.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = HPText.GetComponent<CanvasGroup>();

        if (panelCanvasGroup == null) panelCanvasGroup = HPPanel.AddComponent<CanvasGroup>();
        if (textCanvasGroup == null) textCanvasGroup = HPText.AddComponent<CanvasGroup>();

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
        Push.SetActive(true);
        Debug.Log("Конец изменения");
    }
}
