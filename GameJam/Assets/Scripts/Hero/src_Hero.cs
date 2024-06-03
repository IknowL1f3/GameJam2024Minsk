using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    #region Singleton
    private static Hero _instance;

    private Hero()
    {
        // Initialize default values if necessary
        hp = 100;
        damageMelee = 10f;
        Karma = 0f;
        skill_Heal = 0f;
        skill_Otbras = 0f;
        skill_Fireball = 0f;
    }

    public static Hero Instance()
    {
        return new Hero();
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
