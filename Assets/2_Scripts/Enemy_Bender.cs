using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bender : EnemyBase
{
    [SerializeField] GameObject hand;
    [SerializeField] GameObject _handr;
    [SerializeField] GameObject _handl;
    [SerializeField] Transform handrspawn;
    [SerializeField] Transform handlspawn;
    [SerializeField] float rangeadvance;
    [SerializeField] float speedrotation;
    [SerializeField] float timeexpose;
    [SerializeField] float timespin;
    [SerializeField] bool spin;
    [SerializeField] bool walkagain;
    [SerializeField] bool _init;
    [SerializeField] Transform bendermodel;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
                Debug.Log("atacando y exponiendose");
                counter += Time.deltaTime;
                if (counter <= cooldown)
                {
                    
                    if (spin)
                    {
                        Debug.Log("spin ");
                        Spin();
                    }
                    else
                    {
                        Debug.Log("sin bateria  ");
                        Exposed();
                    }
                }
                else
                {
                    ChangeState();
                }
                
                if(distance >= rangeadvance && spin)
                {
                    Debug.Log("dentro del rango de avance");
                    walkagain = true;
                    Persecution();
                }
                else
                {
                    Debug.Log("fuera del rango de avance");
                    walkagain = false;
                    Persecution();
                }
            }
            else
            {
                Debug.Log("fuera del alcance");
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
            Debug.Log("avanzo");
            agent.isStopped = false;
        }
        else
        {
            Debug.Log("no avanzo");
            agent.isStopped = true;
        }
    }

    void Spin()
    {
        defending = true;
        bendermodel.Rotate(bendermodel.up, speedrotation * Time.deltaTime);
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
