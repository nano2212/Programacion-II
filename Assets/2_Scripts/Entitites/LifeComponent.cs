using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComponent 
{
    [SerializeField] int life = 0;
    [SerializeField] int maxlife = 0;
    public LifeComponent(int _initiallife, int _maxlife)
    {
        life = _initiallife;
        maxlife = _maxlife;
    }
    
    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            if (value > maxlife)
            {
                life = maxlife;
            }
            else return;
            if (value < 0)
            {
                life = 0;
            }
            else return;
            
            life = value;
            
        }
    }

    public void Death()
    {

    }

    public void Hit(int dmg)
    {
        life -= dmg;
    }

    public void Heal(int heal)
    {
        life += heal;
    }
}
