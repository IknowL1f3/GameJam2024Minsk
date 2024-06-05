using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FBSkill : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Text;
    public GameObject Player; // ������ ������
    public Image imageComponent; // ��������� Image, �������� �������� ����� ��������
    public Sprite newSprite; // ����� ��������
    public float fadeDuration = 1f; // ����������������� �������� �����������
    private bool mozno;
    private bool FHP;
    private Text TextComponent;
    private Hero hero;
    public GameObject Exit;
    void Start()
    {
        TextComponent = Text.GetComponent<Text>();
        hero = Hero.Instance;
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

        GiveSoulAndUnlockHealing();
    }

    void GiveSoulAndUnlockHealing()
    {
        mozno = true;
        hero._souls += 50;

    }
    private void Update()
    {
        UpdateText();
        if (mozno && Input.GetKeyDown(KeyCode.Space))
        {
            imageComponent.sprite = newSprite;
            mozno = false;
            FHP = true;
            
        }
        if (FHP && Input.GetKeyDown(KeyCode.Space) && hero.countFireballs>=2)
        {
            StartCoroutine(FadeOutElements());

            FHP = false;
        }
    }
    IEnumerator FadeOutElements()
    {
        yield return new WaitForSeconds(3f);
        
        Debug.Log("������ ���������");
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
        Debug.Log("����� ���������");
        Exit.SetActive(true);
    }
    private void UpdateText()
    {
        if (TextComponent != null)
        {
            TextComponent.text = $"���� � ��������� ����������� FireBall\r\n(Space) \n({hero.countFireballs}/{2})";
        }
    }
}
