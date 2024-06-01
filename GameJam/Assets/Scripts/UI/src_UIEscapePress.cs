using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePress : MonoBehaviour

{

    public bool isOpened = false;
    public GameObject Pause_Menu;
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
        }

    }
}