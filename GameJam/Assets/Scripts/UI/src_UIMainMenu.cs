using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject Panel;
    public float fadeDuration = 5f;
    public VideoPlayer videoPlayer; // VideoPlayer на Canvas
    public RawImage videoDisplay; // RawImage для отображения видео
    public AudioSource musicAudioSource; // AudioSource для музыки

    private int levelToLoad;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd; // Добавляем обработчик для окончания видео
        videoDisplay.gameObject.SetActive(false); // Скрываем RawImage до начала видео
    }

    public void playGame()
    {
        StartCoroutine(FadeInElements());
    }

    public void exitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeInElements()
    {
        CanvasGroup panelCanvasGroup = Panel.GetComponent<CanvasGroup>();
        if (panelCanvasGroup == null) panelCanvasGroup = Panel.AddComponent<CanvasGroup>();

        float elapsedTime = 0f;
        float initialVolume = musicAudioSource.volume;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            panelCanvasGroup.alpha = alpha;

            // Плавное уменьшение громкости музыки
            musicAudioSource.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / fadeDuration);

            yield return null;
        }

        panelCanvasGroup.alpha = 1f;
        musicAudioSource.volume = 0f;

        yield return new WaitForSeconds(1f);

        videoDisplay.gameObject.SetActive(true); // Показываем RawImage
        videoPlayer.Play(); // Запускаем видео
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(levelToLoad);
    }
}
