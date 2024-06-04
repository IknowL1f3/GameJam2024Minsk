using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulCount : MonoBehaviour
{
    private Hero hero;
    public Text text;

    private void Start()
    {
        hero = Hero.Instance;
    }
    void Update()
    {
        text.text = hero._souls.ToString();
    }
}
