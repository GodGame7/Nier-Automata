using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float atk;
    public float def;

    public int _hp;
    public int maxHp = 100;
    public int hp
    {
        get => _hp;
        set
        {
            if(value < 0)
            {
                _hp = 0;
            }
            else if(value > maxHp)
            {
                _hp = maxHp;
            }
            else
            {
                _hp = value;
            }
        }
    }

    public Inventory inven;
    public Weapon weapon;
}
