using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePress : MonoBehaviour

{

    public bool pauseGame;
    public bool OpenSettings;
    public GameObject Pause_Menu;
    public GameObject Settings;
    public GameObject Hints;
    public GameObject HP;
    public GameObject Karma;
    public GameObject BackGround;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Settings.activeSelf!=true)
            {
            if (pauseGame)
                {
                 Resume();
                }
                else
                {
                Pause();
                }
            }
            else
            {
                Pause_Menu.SetActive(true);
                Settings.SetActive(false);
            }
            
        }
    }
    public void Resume()
    {
        Pause_Menu.SetActive(false);
        BackGround.SetActive(false);
        Hints.SetActive(true);
        HP.SetActive(true);
        Karma.SetActive(true);
        Time.timeScale = 1.0f;
        pauseGame= false;
}
    public void Pause()
    {
        Pause_Menu.SetActive(true);
        BackGround.SetActive(true);
        Hints.SetActive(false);
        HP.SetActive(false);
        Karma.SetActive(false);
        Time.timeScale = 0f;
        pauseGame = true;
    }
}