using UnityEngine;
using UnityEngine.UI;

public class MarkerCollector : MonoBehaviour
{
    public static int markersCollected = 0;
    public static int markersToCollect = 3;
    public GameObject Interface;
    public GameObject WASD;
    public GameObject[] uiElements;
    public GameObject[] TextElements;
    private int currentElementIndex = 0;
    public FadeInAttack fadeInAttack;
    public static void AddMarker()
    {
        markersCollected++;
    }

    private void Update()
    {
        if (markersCollected >= markersToCollect)
        {
            HandleMarkersCollected();
            markersCollected = 0;
        }

        if (Interface.activeSelf && Input.GetMouseButtonDown(0)) // Проверка нажатия ЛКМ
        {
            ShowNextElement();
        }
    }

    private void HandleMarkersCollected()
    {
        Interface.SetActive(true);
        Time.timeScale = 0;
        WASD.SetActive(false);

        // Скрываем все элементы, кроме первого
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(i == currentElementIndex);
            TextElements[i].SetActive(i == currentElementIndex);
        }
    }

    private void ShowNextElement()
    {
        // Скрываем текущий элемент
        uiElements[currentElementIndex].SetActive(false);
        TextElements[currentElementIndex].SetActive(false);
        // Переходим к следующему элементу
        currentElementIndex++;

        if (currentElementIndex >= uiElements.Length)
        {
            ResumeGame();
        }
        else
        {
            uiElements[currentElementIndex].SetActive(true);
            TextElements[currentElementIndex].SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Interface.SetActive(false);
        Time.timeScale = 1;

        // Восстанавливаем все скрытые элементы
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(true);
        }

        currentElementIndex = 0; // Сбрасываем индекс для следующего использования
        fadeInAttack.Start1();
    }
}
