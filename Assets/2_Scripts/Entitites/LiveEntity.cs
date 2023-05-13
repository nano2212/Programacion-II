using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveEntity : MonoBehaviour
{
    [SerializeField] int maxlife = 100;
    LifeComponent lifecomponent;

    void Start()
    {
        lifecomponent = new LifeComponent(maxlife, maxlife);
    }

    public virtual void Death()
    {

    }

    public virtual void Hit(int dmg)
    {
        lifecomponent.Hit(dmg);
    }

    public void Heal(int heal)
    {
        lifecomponent.Heal(heal);
    }
}
