using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : LifeEntity
{
    [Header("Compónentes")]
    public NavMeshAgent agent;
    public GameObject hitbox;

    [Header("Referencias")]
    protected Transform target;
    protected PlayerController playerC;
    [SerializeField] protected LayerMask layermask;

    [Header("Booleanos")]
    public bool playeronview;
    public bool isDead;

    [Header("Distancias")]
    [SerializeField] protected float rangeview;
    [SerializeField] protected float rangeattack;
    [SerializeField] protected float distrecoil;
    [SerializeField] protected float distattack;

    [Header("Tiempos y velocidad")]
    [SerializeField] protected float cooldown;
    [SerializeField] protected float cooldownref;
    [SerializeField] protected float counter;
    [SerializeField] protected float smoothtarget;

    [Header("otros")]
    public Renderer rendermodel;

    protected override void Start()
    {
        base.Start();
        EnemyManager.instance.enemies.Add(this);
        agent = GetComponent<NavMeshAgent>();
        playerC = GameManager.instance.player;
        target = playerC.transform;
    }
    // Update is called once per frame
    protected override void Update()
    {
        RaycastHit hit;
        Vector3 dir = target.position - transform.position;
        if (!Physics.Raycast(transform.position, target.position - transform.position, out hit, rangeattack, layermask))
        {
            playeronview = true;
        }
        else
        {
            playeronview = false;
        }

        
        base.Update();
    }
    protected virtual void Attack()
    {
        
    }
    protected virtual void IdleState()
    {

    }
    protected virtual void Persecution()
    {
        agent.SetDestination(target.position);
    }
    protected virtual void Defense()
    {
        rendermodel.material.SetColor("_Color", Color.blue);
        Debug.Log("defensa");
        defending = true;
    }
    protected override void Death()
    {
        base.Death();
        EnemyManager.instance.enemies.Remove(this);
        isDead = true;
    }

    //Funciones clase abstracta PlayObject
    #region
    protected override void OnInitialize()
    {
    }

    protected override void OnDeInitialize()
    {
    }

    protected override void OnResume()
    {
    }

    protected override void OnPause()
    {
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnFixedUpdate()
    {
    }

    protected override void OnLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
