using UnityEngine;

public class CameraFollow : MonoBehaviour
{
<<<<<<< HEAD
    public Transform target;
   
} 
=======
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
>>>>>>> 63a6d828db8d3fea27f03fa6190b020cc6c9e862
