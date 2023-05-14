using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.LifeSystem;

public class LifeEntity : MonoBehaviour
{
    LifeComponent life;
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
        Destroy(gameObject);
        Debug.Log("Morido");
    }

    public virtual void TakeDamage(int dmg)
    {
        Debug.Log("Daña");
        life.Life -= dmg;
        Debug.Log(life.Life);
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        if (life.Life == 0)
        {
            Debug.Log(life.Life + this.gameObject.name + " debe morir");
            Death();
        }
    }
}
