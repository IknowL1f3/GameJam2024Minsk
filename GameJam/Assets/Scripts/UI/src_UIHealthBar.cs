using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{

    public float maxValue = 100;
    public Color color = Color.red;
    public int width = 4;
    public UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Image backgroundBar;
    public bool isRight;
    public GameObject Knight_bomj;
    private static float current;

    void Start()
    {
        slider.fillRect.GetComponent<UnityEngine.UI.Image>().color = color;
        slider.maxValue = maxValue;
        slider.minValue = 0;
        current = maxValue;
        UpdateUI();
    }

    public static float currentValue
    {
        get { return current; }
    }

    void Update()
    {
        if (current <= 0)
        {
            Debug.Log("Помер");
            Knight_bomj.SetActive(false);// поменять на смерть
            current = 0;
            
        }
        if (current > maxValue) current = maxValue;
        slider.value = current;
        UpdateBackgroundBar();
    }

    void UpdateUI()
    {
        RectTransform rect = slider.GetComponent<RectTransform>();
        int rectDeltaX = Screen.width / width;
        float rectPosX = 0;

        if (isRight)
        {
            rectPosX = rect.position.x - (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = UnityEngine.UI.Slider.Direction.RightToLeft;
        }
        else
        {
            rectPosX = rect.position.x + (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = UnityEngine.UI.Slider.Direction.LeftToRight;
        }

        rect.sizeDelta = new Vector2(rectDeltaX, rect.sizeDelta.y);
        rect.position = new Vector3(rectPosX, rect.position.y, rect.position.z);

        RectTransform bgRect = backgroundBar.GetComponent<RectTransform>();
        bgRect.sizeDelta = rect.sizeDelta;
        bgRect.position = rect.position;
    }

    public static void AdjustCurrentValue(float adjust)
    {
        current = adjust;
    }
    void UpdateBackgroundBar()
    {

        RectTransform bgRect = backgroundBar.GetComponent<RectTransform>();
        float healthPercentage = current / maxValue;

    }
}