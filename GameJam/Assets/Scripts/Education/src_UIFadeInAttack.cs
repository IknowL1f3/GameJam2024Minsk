using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInAttack : MonoBehaviour
{
    public MovementHero movementHero;
    public GameObject AttackPanel;
    public GameObject AttackText;
    public float fadeDuration = 0.1f;
    private Text AttackTextComponent;

    private int collectedCylinders = 0;
    private int totalCylinders = 3; // ���������� ����� ���������� ���������
    
    public void Start1()
    {
        AttackTextComponent = AttackText.GetComponent<Text>();
        StartCoroutine(FadeInElements());
        UpdateAttackText(); // ���������� ������ ��� ������
        Debug.Log("������ �������");
    }
    private void Update()
    {
        IncrementCylinderCounter();
    }

    IEnumerator FadeInElements()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("������ ���������");
        CanvasGroup panelCanvasGroup = AttackPanel.GetComponent<CanvasGroup>();
        CanvasGroup textCanvasGroup = AttackText.GetComponent<CanvasGroup>();

        if (panelCanvasGroup == null) panelCanvasGroup = AttackPanel.AddComponent<CanvasGroup>();
        if (textCanvasGroup == null) textCanvasGroup = AttackText.AddComponent<CanvasGroup>();

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
        Debug.Log("����� ���������");
    }

    public void IncrementCylinderCounter()
    {
        collectedCylinders++;
        UpdateAttackText();
    }

    private void UpdateAttackText()
    {
        if (AttackTextComponent != null)
        {
            AttackTextComponent.text = $"������� ����� ��������� ���. \n{movementHero.numberOfAttack}/3";
            if(movementHero.numberOfAttack == 3)
            {
                Debug.Log("complete");
            }
        }
    }
}
