using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Prohod : MonoBehaviour
{
    public GameObject DoorPrefab;
    public string playerTag = "Player";
    public VideoPlayer videoPlayer; // VideoPlayer �� Canvas
    public RawImage videoDisplay; // RawImage ��� ����������� �����
    private int levelToLoad;
    private Canvas videoCanvas;
    public GameObject Cube; // �������� ��� �� GameObject ��� ��������� ������� � �����������

    void Start()
    {
        videoDisplay.gameObject.SetActive(false); // �������� RawImage �� ������ �����
        // ���������, ��� � ����� ���������� Canvas
        videoCanvas = videoDisplay.GetComponentInParent<Canvas>();
        if (videoCanvas != null)
        {
            videoCanvas.sortingOrder = 100; // ������������� ������� ������� ����������
        }

        // ��������� ���������� ������� ��������� �����
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("��������");
            DoorPrefab.SetActive(false);
            videoDisplay.gameObject.SetActive(true); // ���������� RawImage
            videoPlayer.Play(); // ��������� �����
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        if (videoCanvas != null)
        {
            videoCanvas.gameObject.SetActive(false); // �������� Canvas �� ��������� �����
        }

        // ��������� ���������� � ����
        Collider[] colliders = Cube.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
