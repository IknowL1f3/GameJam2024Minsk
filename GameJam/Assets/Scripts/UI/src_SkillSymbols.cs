using UnityEngine;
using UnityEngine.UI;

public class TextColorChanger : MonoBehaviour
{
    public Text[] textComponents; // Массив компонентов текста, цвет которых нужно изменить
    private Color greenColor = Color.green; // Цвет зеленый

    private int currentTextIndex = 0; // Индекс текущего текста, который будет меняться при нажатии клавиш

    private void Update()
    {
        // Проверяем нажатие клавиш Space, H, G
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeTextColor(0, greenColor); // Изменяем цвет текущего текста на зеленый

        }else
            if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeTextColor(0, new Color(178 / 255f, 112 / 255f, 0)); // Изменяем цвет текущего текста на зеленый
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeTextColor(1, greenColor); // Изменяем цвет текущего текста на зеленый
        }
        else
            if (Input.GetKeyUp(KeyCode.G))
        {
            ChangeTextColor(1, new Color(178 / 255f, 112 / 255f, 0)); // Изменяем цвет текущего текста на зеленый
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeTextColor(2, greenColor); // Изменяем цвет текущего текста на зеленый
        }
        else
            if (Input.GetKeyUp(KeyCode.H))
        {
            ChangeTextColor(2, new Color(178 / 255f, 112 / 255f, 0)); // Изменяем цвет текущего текста на зеленый
        }
    }

    private void ChangeTextColor(int index, Color color)
    {
            textComponents[index].color = color;
    }
}
