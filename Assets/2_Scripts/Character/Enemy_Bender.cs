using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bender : EnemyBase
{
    [SerializeField] GameObject hand;
    [SerializeField] GameObject _handr;
    [SerializeField] AudioManager au_handr;
    [SerializeField] GameObject _handl;
    [SerializeField] AudioManager au_handl;
    [SerializeField] Transform handrspawn;
    [SerializeField] Transform handlspawn;
    [SerializeField] float rangeadvance;
    [SerializeField] float speedrotation;
    [SerializeField] float timeexpose;
    [SerializeField] float timespin;
    [SerializeField] bool spin;
    [SerializeField] float cont;
    [SerializeField] float refcont;
    [SerializeField] bool walkagain;
    [SerializeField] bool _init;
    [SerializeField] Transform bendermodel;
    public AudioManager_Enemy audiomanager;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        audiomanager = GetComponent<AudioManager_Enemy>();
        cooldown = timespin;
        counter = timespin;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        var distance = Vector3.Distance(target.position, transform.position);
        if (!isDead)
        {
            
            
            if(playeronview && distance <= rangeattack)
            {
                counter += Time.deltaTime;
                if (counter <= cooldown)
                {
                    
                    if (spin)
                    {
                        Spin();
                    }
                    else
                    {
                        Exposed();
                    }
                }
                else
                {
                    ChangeState();
                }
                
                if(distance >= rangeadvance && spin)
                {
                    walkagain = true;
                    Persecution();
                }
                else
                {
                    walkagain = false;
                    Persecution();
                }
            }
            else
            {
                rendermodel.material.SetColor("_Color", Color.white);
                IdleState();
            }

        }
    }
    protected override void Attack()
    {
         
        
    }

    protected override void Persecution()
    {
        agent.SetDestination(target.position);
        if (walkagain)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void Spin()
    {
        defending = true;
        bendermodel.Rotate(bendermodel.up, speedrotation * Time.deltaTime);
        cont += Time.deltaTime;
        if (cont > refcont)
        {
            audiomanager.PlaySound("spin");
            cont = 0;
        }
    }
    void Exposed()
    {
        defending = false;
    }
    void ChangeState()
    {
        counter = 0;
        spin = !spin;
        if (spin)
        {
            _handr = Instantiate(hand, handrspawn);
            _handl = Instantiate(hand, handlspawn);
            _handl.GetComponent<HitBox_2>().au_manager = audiomanager;
            _handr.GetComponent<HitBox_2>().au_manager = audiomanager;
            rendermodel.material.SetColor("_Color", Color.blue);
            defending = true;
            cooldown = timespin;
        }
        else
        {
            Destroy(_handr);
            Destroy(_handl);
            rendermodel.material.SetColor("_Color", Color.red);
            defending = false;
            cooldown = timeexpose;
        }
    }
}
