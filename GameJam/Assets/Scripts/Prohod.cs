using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Prohod : MonoBehaviour
{
    public GameObject DoorPrefab;
    public string playerTag = "Player";
    public VideoPlayer videoPlayer; // VideoPlayer на Canvas
    public RawImage videoDisplay; // RawImage для отображения видео
    private int levelToLoad;
    private Canvas videoCanvas;
    public GameObject Cube; // Измените тип на GameObject для получения доступа к компонентам

    void Start()
    {
        videoDisplay.gameObject.SetActive(false); // Скрываем RawImage до начала видео
        // Убедитесь, что у видео правильный Canvas
        videoCanvas = videoDisplay.GetComponentInParent<Canvas>();
        if (videoCanvas != null)
        {
            videoCanvas.sortingOrder = 100; // Устанавливаем высокий порядок рендеринга
        }

        // Добавляем обработчик события окончания видео
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Анимация");
            DoorPrefab.SetActive(false);
            videoDisplay.gameObject.SetActive(true); // Показываем RawImage
            videoPlayer.Play(); // Запускаем видео
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (videoCanvas != null)
        {
            videoCanvas.gameObject.SetActive(false); // Скрываем Canvas по окончании видео
        }

        // Отключаем коллайдеры у куба
        Collider[] colliders = Cube.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
