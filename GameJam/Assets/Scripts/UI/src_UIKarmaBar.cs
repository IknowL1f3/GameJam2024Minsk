using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Karma : MonoBehaviour
{

    public float maxValue = 100;
    public Color color = new Color(255,255,255);
    public float width = 4;
    public Slider slider;
    public Image backgroundBar;
    public bool isRight;

    private static float current;

    void Start()
    {
        slider.fillRect.GetComponent<Image>().color = color;

        slider.maxValue = maxValue;
        slider.minValue = 0;
        current = 100;

        UpdateUI();
    }

    public static float currentValue
    {
        get { return current; }
    }

    void Update()
    {
        if (current < 0) current = 0;
        if (current > maxValue) current = maxValue;
        slider.value = current;
        UpdateBackgroundBar();
    }

    void UpdateUI()
    {
        RectTransform rect = slider.GetComponent<RectTransform>();

        float rectDeltaX = Screen.width / width;
        float rectPosX = 0;

        if (isRight)
        {
            rectPosX = rect.position.x - (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = Slider.Direction.RightToLeft;
        }
        else
        {
            rectPosX = rect.position.x + (rectDeltaX - rect.sizeDelta.x) / 2;
            slider.direction = Slider.Direction.LeftToRight;
        }

        rect.sizeDelta = new Vector2(rectDeltaX, rect.sizeDelta.y);
        rect.position = new Vector3(rectPosX, rect.position.y, rect.position.z);

        RectTransform bgRect = backgroundBar.GetComponent<RectTransform>();
        bgRect.sizeDelta = rect.sizeDelta;
        bgRect.position = rect.position;
    }

    public static void AdjustCurrentValue(float adjust)
    {
        current += adjust;
    }
    void UpdateBackgroundBar()
    {

        RectTransform bgRect = backgroundBar.GetComponent<RectTransform>();
        float karmaPercentage = current / maxValue;

    }
}