using UnityEngine;
using UnityEngine.UI;

public class TextColorChanger : MonoBehaviour
{
    public Text[] textComponents; // ������ ����������� ������, ���� ������� ����� ��������
    private Color greenColor = Color.green; // ���� �������

    private int currentTextIndex = 0; // ������ �������� ������, ������� ����� �������� ��� ������� ������

    private void Update()
    {
        // ��������� ������� ������ Space, H, G
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeTextColor(0, greenColor); // �������� ���� �������� ������ �� �������

        }else
            if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeTextColor(0, new Color(178 / 255f, 112 / 255f, 0)); // �������� ���� �������� ������ �� �������
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeTextColor(1, greenColor); // �������� ���� �������� ������ �� �������
        }
        else
            if (Input.GetKeyUp(KeyCode.G))
        {
            ChangeTextColor(1, new Color(178 / 255f, 112 / 255f, 0)); // �������� ���� �������� ������ �� �������
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeTextColor(2, greenColor); // �������� ���� �������� ������ �� �������
        }
        else
            if (Input.GetKeyUp(KeyCode.H))
        {
            ChangeTextColor(2, new Color(178 / 255f, 112 / 255f, 0)); // �������� ���� �������� ������ �� �������
        }
    }

    private void ChangeTextColor(int index, Color color)
    {
            textComponents[index].color = color;
    }
}
