using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LifeEntity
{
    public PlayerController playerC;
    Transform target;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerC = GameManager.instance.player;
        target = playerC.transform;
    }

    protected virtual void Attack()
    {
        
    }

    protected override void Death()
    {
        base.Death();
        playerC.focus = false;
        playerC.anim.SetBool("focus", false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //agent.SetDestination(target.position);
    }
    private void Persecution()
    {

    }
}
