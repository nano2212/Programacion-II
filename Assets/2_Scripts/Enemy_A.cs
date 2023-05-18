using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_A : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack()
    {
        agent.Move(new Vector3(1,0,0));

    }
}
