using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    #region Singleton
    public static Hero __instance;

    private void Awake()
    {
        __instance = this;
    }
    #endregion

    public GameObject hero;
}
