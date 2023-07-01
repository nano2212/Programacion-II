using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;

public abstract class LifeEntity : PlayObject
{
    public LifeComponent life;
    public int lives = 1;
    public bool defending;
    [SerializeField] int maxLife;

    protected virtual void Awake()
    {
        life = new LifeComponent(maxLife, maxLife);
    }

    protected virtual void Start()
    {
        
    }
    protected virtual void Death()
    {

        Destroy(this.gameObject);
    }

    public virtual void TakeDamage(int dmg)
    {
        if (!defending)
        {
            life.Life -= dmg;
        }
        
    }

    public virtual void Heal(int hp)
    {
        life.Life += hp;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (life.Life == 0)
        {
            Death();
        }
    }
}
