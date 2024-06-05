using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PushSkill: MonoBehaviour
{
    public GameObject Panel;
    public GameObject Text;
    public GameObject Player; // Объект игрока
    public Image imageComponent; // Компонент Image, текстура которого будет изменена
    public Sprite newSprite; // Новая текстура
    public float fadeDuration = 1f; // Продолжительность плавного отображения
    private bool mozno;
    private bool FHP;
    private Text TextComponent;
    private Hero hero;
    public GameObject Zombies;
    public GameObject FB;
    public GameObject zombie;
    void Start()
    {
        TextComponent = Text.GetComponent<Text>();
        hero = Hero.Instance;
        StartCoroutine(ShowElements());
        Vector3 position = Player.transform.position;
        Zombies.transform.position = position;
        Zombies.SetActive(true);
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

        GiveSoulAndUnlockHealing();
    }

    void GiveSoulAndUnlockHealing()
    {
        mozno = true;
        hero._souls += 50;

    }
    private void Update()
    {
        if (mozno && Input.GetKeyDown(KeyCode.H))
        {
            imageComponent.sprite = newSprite;
            mozno = false;
            FHP = true;
        }
        if (zombie.transform.position.x!= 1.050003 && FHP && Input.GetKeyDown(KeyCode.H) && hero.isPushActive)
        {
            StartCoroutine(FadeOutElements());
            
            FHP = false;
        }
    }
    IEnumerator FadeOutElements()
    {
        yield return new WaitForSeconds(3f);
        Destroy(Zombies);
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
        FB.SetActive(true);
        Debug.Log("Конец изменения");
    }
}
