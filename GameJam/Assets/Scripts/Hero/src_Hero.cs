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

    public int hp = 100;

    public float damageMelee;

    public float Karma;

    public float skill_Heal;

    public float skill_Otbras;
  
    public float skill_Fireball;

    public void GetDamage(int countOfDamage)
    {
        hp -= countOfDamage;
        Debug.Log("хп проебано");
    }


}
