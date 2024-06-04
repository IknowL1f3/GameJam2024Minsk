using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    #region SingletonHero
    private static Hero _instance;
    private static readonly object _lock = new object();

    [Header("Настройки персонажа")]
    public float damageMelee;
    public int _hp;
    public int _souls;
    public int _karma;

    [Header("Открытие способностей")]
    [SerializeField]
    private bool _fireball;
    public bool _push;
    public bool _heal;

    private Hero()
    {
        hp = 100;
        damageMelee = 10f;
        _karma = 0;
        _souls = 0;
        _fireball = false;
        _push = false;
        _heal = false;
    }

    public static Hero Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<Hero>();
                    }
                }
            }
            return _instance;
        }
    }
    #endregion

    public int hp
    {
        get
        {
            lock (_lock)
            {
                return _hp;
            }
        }
        private set
        {
            lock (_lock)
            {
                _hp = value;
            }
        }
    }
    public int souls
    {
        get
        {
            lock (_lock)
            {
                return _souls;
            }
        }
        private set
        {
            lock (_lock)
            {
                _souls = value;
            }
        }
    }
    public int karma
    {
        get
        {
            lock (_lock)
            {
                return _karma;
            }
        }
        set
        {
            lock (_lock)
            {
                _karma = value;
            }
        }
    }

    public bool fireball
    {
        get
        {
            lock (_lock)
            {
                return _fireball;
            }
        }
        set
        {
            lock (_lock)
            {
                _fireball = value;
            }
        }
    }
    public bool heal
    {
        get
        {
            lock (_lock)
            {
                return _heal;
            }
        }
        set
        {
            lock (_lock)
            {
                _heal = value;
            }
        }
    }
    public bool push
    {
        get
        {
            lock (_lock)
            {
                return _push;
            }
        }
        set
        {
            lock (_lock)
            {
                _push = value;
            }
        }
    }

    public GameObject hero;

    public void GetDamage(int countOfDamage)
    {
        hp -= countOfDamage;
        Debug.Log("хп проебано");
    }

    public void GetSoul(int countOfSoul)
    {
        _souls += countOfSoul;
        Debug.Log($"Души:{souls}");
    }

    public void GetKarma(int countOfKarma)
    {
        if (countOfKarma < 0)
            _karma = _karma + countOfKarma < 0 ? _karma = 0 : _karma += countOfKarma;
        else if (countOfKarma + _karma > 100)
            _karma = 100;
        else
            _karma += countOfKarma;
        Debug.Log($"карма:{_karma}");
    }

    public void Heal(int heal)
    {
        if (_hp + heal > 100)
            _hp = 100;
        else
            _hp += heal;
    }
}
