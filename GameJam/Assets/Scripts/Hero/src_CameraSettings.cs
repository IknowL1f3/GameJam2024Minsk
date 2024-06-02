using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // ���������� ��� �������� ������ �� ������
    public Transform player;

    // ���������� ����� ������� � ������� �� ������
    public float height = 10.0f;

    // �������� ������ �� ����������� (���� �����)
    public Vector3 offset = new Vector3(0, 0, 0);

    // ���������� ������ ������ ����
    void LateUpdate()
    {
        if (player != null)
        {
            // ������������� ������� ������ ��� ������� � ������ ��������
            Vector3 newPosition = player.position + offset;
            newPosition.y += height;
            transform.position = newPosition;
        }
    }
}
