using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.LifeSystem
{
    public class LifeComponent
    {
        [SerializeField] int life = 100;
        [SerializeField] int maxlife = 100;
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
                else if (value < 0)
                {
                    life = 0;
                }
                else
                {
                    life = value;
                }
            }
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
}

