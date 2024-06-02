using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePress : MonoBehaviour

{

    public bool isOpened = false;
    public GameObject Pause_Menu;
    public GameObject Hints;
    public GameObject HP;
    public GameObject Karma;
    public void IO()
    {
        isOpened = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpened = !isOpened; 
            Pause_Menu.SetActive(isOpened); 
            Hints.SetActive(!isOpened);
            HP.SetActive(!isOpened);
            Karma.SetActive(!isOpened);
        }

    }
}