using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Karma : MonoBehaviour
{

    public int maxValue = 100;
    public Color color = new Color(134, 15, 134);
    public int width = 4;
    public Slider slider;
    public Image backgroundBar;
    public bool isRight;

    private Hero hero;

    private static float current;

    void Start()
    {
        hero = Hero.Instance;

        slider.fillRect.GetComponent<Image>().color = color;

        slider.maxValue = maxValue;
        slider.minValue = 0;
        current = 0;

        UpdateUI();
    }

    public static float currentValue
    {
        get { return current; }
    }

    void Update()
    {
        if (hero.karma < 0) hero.karma = 0;
        if (hero.karma > maxValue) hero.karma = maxValue;
        slider.value = hero.karma;
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