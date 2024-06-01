using UnityEngine;

public class CameraFollow : MonoBehaviour
{
<<<<<<< HEAD
    public Transform target;
   
} 
=======
    // ѕеременна€ дл€ хранени€ ссылки на игрока
    public Transform player;

    // –ассто€ние между камерой и игроком по высоте
    public float height = 10.0f;

    // —мещение камеры по горизонтали (если нужно)
    public Vector3 offset = new Vector3(0, 0, 0);

    // ќбновление камеры каждый кадр
    void LateUpdate()
    {
        if (player != null)
        {
            // ”станавливаем позицию камеры над игроком с учетом смещени€
            Vector3 newPosition = player.position + offset;
            newPosition.y += height;
            transform.position = newPosition;
        }
    }
}
>>>>>>> 63a6d828db8d3fea27f03fa6190b020cc6c9e862
