using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject Panel;
    public float fadeDuration = 5f;
    public VideoPlayer videoPlayer; // VideoPlayer �� Canvas
    public RawImage videoDisplay; // RawImage ��� ����������� �����
    public AudioSource musicAudioSource; // AudioSource ��� ������

    private int levelToLoad;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd; // ��������� ���������� ��� ��������� �����
        videoDisplay.gameObject.SetActive(false); // �������� RawImage �� ������ �����
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

            // ������� ���������� ��������� ������
            musicAudioSource.volume = Mathf.Lerp(initialVolume, 0f, elapsedTime / fadeDuration);

            yield return null;
        }

        panelCanvasGroup.alpha = 1f;
        musicAudioSource.volume = 0f;

        yield return new WaitForSeconds(1f);

        videoDisplay.gameObject.SetActive(true); // ���������� RawImage
        videoPlayer.Play(); // ��������� �����
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(levelToLoad);
    }
}
