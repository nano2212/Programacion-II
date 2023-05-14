using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LifeEntity
{
    public PlayerController playerRef;
    Transform target;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerRef = GameManager.instance.player;
        target = playerRef.transform;
    }

    protected virtual void Attack()
    {
        
    }

    protected override void Death()
    {

    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
}
