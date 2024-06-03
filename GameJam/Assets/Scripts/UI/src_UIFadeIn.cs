using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInUI : MonoBehaviour
{
    public GameObject wasdPanel;
    public GameObject wasdText;
    public float fadeDuration = 1.0f;
    private Text wasdTextComponent;

    private int collectedCylinders = 0;
    private int totalCylinders = 3; // Установите общее количество цилиндров

    void Start()
    {
        wasdTextComponent = wasdText.GetComponent<Text>();
        StartCoroutine(FadeInElements());
        UpdateWasdText(); // Обновление текста при старте
        Debug.Log("Скрипт запущен");
    }

    void Update()
    {
        HandleKeyPress(KeyCode.W);
        HandleKeyPress(KeyCode.A);
        HandleKeyPress(KeyCode.S);
        HandleKeyPress(KeyCode.D);
    }

    IEnumerator FadeInElements()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Начало изменения");
        CanvasGroup panelCanvasGroup = wasdPanel.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = wasdText.GetComponent<CanvasGroup>();

        if (panelCanvasGroup == null) panelCanvasGroup = wasdPanel.AddComponent<CanvasGroup>();
        if (textCanvasGroup == null) textCanvasGroup = wasdText.AddComponent<CanvasGroup>();

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
        Debug.Log("Конец изменения");
    }

    private void HandleKeyPress(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            SetKeyColor(key, Color.green);
        }

        if (Input.GetKeyUp(key))
        {
            SetKeyColor(key, Color.white);
        }
    }

    private void SetKeyColor(KeyCode key, Color color)
    {
        if (wasdTextComponent != null)
        {
            string originalText = wasdTextComponent.text;
            string newText = "";

            foreach (char c in originalText)
            {
                if (c.ToString().ToUpper() == key.ToString())
                {
                    newText += $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{c}</color>";
                }
                else
                {
                    newText += c;
                }
            }

            wasdTextComponent.text = newText;
        }
    }

    public void IncrementCylinderCounter()
    {
        collectedCylinders++;
        UpdateWasdText();
    }

    private void UpdateWasdText()
    {
        if (wasdTextComponent != null)
        {
            wasdTextComponent.text = $"собери зелёные метки используя W,A,S,D. \n({collectedCylinders}/{totalCylinders})";
        }
    }
}
