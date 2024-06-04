using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private static readonly Lazy<Hero> _lazyInstance = new Lazy<Hero>(() => new Hero());
    private static readonly object _lock = new object();

    private Hero()
    {
        hp = 100;
        damageMelee = 10f;
        _karma = 0;
        _souls = 0;
    }

    public static Hero Instance
    {
        get
        {
            return _lazyInstance.Value;
        }
    }

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

    public GameObject hero;

    private int _hp;
    private int _souls;
    private int _karma;

    public float damageMelee;



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
