using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;

public class LifeEntity : MonoBehaviour
{
    LifeComponent life;
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
        Debug.Log("Morido");
    }

    public virtual void TakeDamage(int dmg)
    {
        if (!defending)
        {
            life.Life -= dmg;
            Debug.Log("quedan " + life.Life + " HP");
        }
        
    }

    public virtual void Heal(int hp)
    {
        life.Life += hp;
        Debug.Log("sumaste " + hp + " HP");
        Debug.Log("Tenes " + life.Life + " HP");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (life.Life == 0)
        {
            Debug.Log(life.Life + " " + this.gameObject.name + " debe morir");
            Death();
        }
    }
}
