using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEntity : MonoBehaviour
{
    LifeComponent life;
    [SerializeField] int maxLife;

    protected virtual void Start()
    {
        life = new LifeComponent(maxLife, maxLife);
    }
    protected virtual void Death()
    {

    }

    protected virtual void TakeDamage(int dmg)
    {
        life.Life -= dmg;
    }
    

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
