using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Enemy_Racer : EnemyBase
{
    [SerializeField] protected Transform rightspawn;
    [SerializeField] protected Transform leftspawn;

    [SerializeField] protected float cooldownvar;
    [SerializeField] protected float defensetime;
    [SerializeField] protected float cooldowndefense;
    public AudioManager_Enemy audiomanager;

    protected override void Start()
    {
        base.Start();
        audiomanager = GetComponent<AudioManager_Enemy>();
        cooldown = cooldownref + Random.Range(-cooldownvar, cooldownvar);
        agent = GetComponent<NavMeshAgent>();
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
                        defensetime = 0;
                        defending = false;
                        rendermodel.material.SetColor("_Color", Color.white);
                    }
                }

            }
        }
    }

    protected override void Attack()
    {
        var command = (int)Random.Range(0, 3);
        audiomanager.PlaySound("Hit");
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

    protected virtual void RightHand()
    {
        var _hitbox = Instantiate(hitbox, rightspawn);
        _hitbox.GetComponent<hitbox>().au_manager = audiomanager;

    }

    protected virtual void LeftHand()
    {
        var _hitbox = Instantiate(hitbox, leftspawn);
        _hitbox.GetComponent<hitbox>().au_manager = audiomanager;
    }
}
