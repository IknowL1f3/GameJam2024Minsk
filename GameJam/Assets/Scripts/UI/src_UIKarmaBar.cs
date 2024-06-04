using UnityEngine;
using UnityEngine.UI;

public class Karma : MonoBehaviour
{
    public int maxValue = 100;
    public float width;
    public Slider slider;
    public Image backgroundBar;
    public bool isRight;

    private Hero hero;

    private static float current;

    // Начальный цвет белый
    private Color startColor = Color.white;

    // Конечный цвет фиолетовый
    private Color endColor = new Color(134 / 255f, 15 / 255f, 134 / 255f);

    void Start()
    {
        hero = Hero.Instance;

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
        if (Input.GetKeyDown(KeyCode.RightBracket)) // Проверка нажатия клавиши `]`
        {
            AdjustCurrentValue(10);
        }

        hero.karma = Mathf.Clamp(hero.karma, 0, maxValue);
        slider.value = hero.karma;
        UpdateBackgroundBar();
        UpdateSliderColor();
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

    public static void AdjustCurrentValue(int adjust)
    {
        current += adjust;
        Hero.Instance.karma += adjust; // Увеличение кармы героя
    }

    void UpdateBackgroundBar()
    {
        RectTransform bgRect = backgroundBar.GetComponent<RectTransform>();
        float karmaPercentage = current / maxValue;
    }

    void UpdateSliderColor()
    {
        float t = slider.value / maxValue;
        Color newColor = Color.Lerp(startColor, endColor, t);
        slider.fillRect.GetComponent<Image>().color = newColor;
    }
}
