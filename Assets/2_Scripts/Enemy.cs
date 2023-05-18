using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LifeEntity
{
    [Header("Compónentes")]
    public NavMeshAgent agent;
    public GameObject hitbox;    

    [Header("Referencias")]
    protected Transform target;
    PlayerController playerC;
    [SerializeField] protected Transform rightspawn;
    [SerializeField] protected Transform leftspawn;

    [Header("Booleanos")]
    public bool onattack;
    public bool ondefense;
    public bool exposed;
    public bool struck;
    public bool isDead;

    [Header("Distancias")]
    [SerializeField] float rangeview;
    [SerializeField] float rangeattack;
    [SerializeField] protected float distrecoil;
    [SerializeField] protected float distattack;

    [Header("Tiempos y velocidad")]
    [SerializeField] protected float cooldown;
    [SerializeField] protected float cooldownref;
    [SerializeField] protected float cooldownvar;
    [SerializeField] protected float defensetime;
    [SerializeField] protected float cooldowndefense;
    [SerializeField] protected float counter;
    [SerializeField] float speed;
    [SerializeField] protected float smoothtarget;

    [Header("otros")]
    Renderer render;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        cooldown = cooldownref + Random.Range(-cooldownvar, cooldownvar);
        agent = GetComponent<NavMeshAgent>();
        render = GetComponent<Renderer>();
        playerC = GameManager.instance.player;
        target = playerC.transform;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        var distance = Vector3.Distance(target.position, transform.position);
        if (!isDead)
        {
            if (distance >= rangeview)
            {
                agent.isStopped = false;
                IdleState();
            }
            else if (distance >= rangeattack)
            {
                agent.isStopped = false;
                Persecution();
            }
            else
            {
                agent.isStopped = true;
                if (!defending)
                {
                    counter += Time.deltaTime;
                    if (counter >= cooldown)
                    {
                        Attack();
                        counter = 0;
                        cooldown = cooldownref + Random.Range(-cooldownvar, cooldownvar);
                    }
                }
                else
                {
                    defensetime += Time.deltaTime;
                    if (defensetime >= cooldowndefense)
                    {
                        Debug.Log("tERMINO LA DEFENSA");
                        defensetime = 0;
                        defending = false;
                        render.material.SetColor("_Color", Color.white);
                    }
                }

            }
        }
        
    }
    protected virtual void Attack()
    {
        var command = (int)Random.Range(0, 3);
        print(command);
        switch (command) 
        {
            case 0:
                RightHand();
                break;
            case 1:
                Defense();
                break;
            case 2:
                LeftHand();
                break;
            default:
                break;
        }
    }
    protected virtual void IdleState()
    {

    }
    protected virtual void Persecution()
    {
        agent.SetDestination(target.position);
    }
    protected virtual void RightHand()
    {
        Debug.Log("Piña derecha");
        var _hitbox = Instantiate(hitbox, rightspawn);
        _hitbox.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }
    protected virtual void LeftHand()
    {
        var _hitbox = Instantiate(hitbox, leftspawn);
        _hitbox.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
    }
    protected virtual void Defense()
    {
        render.material.SetColor("_Color", Color.blue);
        Debug.Log("defensa");
        defending = true;
    }
    protected override void Death()
    {
        base.Death();
        isDead = true;
    }
}
