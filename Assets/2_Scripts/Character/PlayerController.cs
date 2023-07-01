using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : LifeEntity
{
    [SerializeField] AudioManager_Player audiomanager;
    [SerializeField] Rigidbody rb;
    [SerializeField] CameraController camcontrol;
    [SerializeField] float forcejump = 2;
    [SerializeField] float speed = 5;
    [SerializeField] float speedRotF = 5;
    [SerializeField] float smoothnessrotation =5;
    [SerializeField] float angle;
    [SerializeField] GameObject hitbox;
    [SerializeField] GameObject instance_hitbox;
    [SerializeField] Transform hitPos;
    [SerializeField] Transform basecapsule;
    [SerializeField] Transform heightcapsule;
    [SerializeField] float radiuscapsule;
    [SerializeField] float actualDist;
    Vector3 direction = new Vector3(0, 0, 0);
    Collider nearestCollider;

    public Transform focuscampos;
    public Transform freecampos;
    public Transform target;
    public Animator anim;
    [SerializeField] LayerMask layermaskA;

    public bool focus;
    public bool ontarget;
    public bool interaction = false;

    protected override void Awake()
    {
        base.Awake();
        audiomanager = GetComponent<AudioManager_Player>();
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        camcontrol = GameManager.instance.camcontrol; 
        camcontrol.InitCam(freecampos);
        actualDist = radiuscapsule;
    }

    // Update is called once per frame
    //protected override void Update()
    //{
    //    base.Update();
    //    //PlayerInputs();
    //    //CameraController();
    //}

    void PlayerInputs()
    {
        //Acciones
        #region 
        if (!defending)
        {
            //Golpe B
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("punchB");
            }
            //Golpe A
            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetTrigger("punchA");
            }
            //Patada
            if (Input.GetButtonDown("Fire3"))
            {
                anim.SetTrigger("kick");
            }
        }

        //Bloqueo
        if (Input.GetButtonDown("Block"))
        {
            anim.SetBool("blocking", true);
            defending = true;
        }
        if (Input.GetButtonUp("Block"))
        {
            anim.SetBool("blocking", false);
            defending = false;
        }
        ////Salto
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    anim.SetTrigger("jump");
        //}
        //Interactuar
        if (Input.GetButtonDown("Interact"))
        {
            interaction = true;
            var nearestinteractable = GetNearestObject();
            if (nearestinteractable == null) return;
            var interactable = nearestinteractable.GetComponent<Iinteractable>();
            if (interactable != null)
            {
                Debug.Log("interactuo");
                interactable.Interact();
                interactable = null;
            }
        }
        #endregion        

        //Cambio de camara
        if (Input.GetButtonDown("FocusMode"))
        {
            camcontrol.endtransition = false;
            if (!focus)
            {
                anim.SetBool("focus", true);
                focus = true;
            }
            else
            {
                focus = false;
                anim.SetBool("focus", false);
            }
        }

    }

    public void StepPlayer()
    {
        audiomanager.PlaySound("step");
    }

    private void PlayerMovements()
    {
        float x = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("x", h);
        anim.SetFloat("y", v);
        

        //Movimiento
        #region

        if (!defending)
        {

            if((v != 0 || h != 0) && !focus)
            {

                anim.SetBool("walk", true);
                direction = camcontrol.transform.forward.normalized * v + camcontrol.transform.right.normalized * h;
                MovePlayer(direction);
                //transform.position += camcontrol.transform.forward * v * speed * Time.deltaTime;
                //transform.position += camcontrol.transform.right * h * speed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward,
                                            new Vector3(camcontrol.transform.forward.x, 0, camcontrol.transform.forward.z),
                                            smoothnessrotation * Time.deltaTime);
            }
            else if((v != 0 || h != 0 || x != 0) && focus)
            {
                anim.SetBool("walk", true);
                direction = transform.forward.normalized * v + camcontrol.transform.right.normalized * h;
                MovePlayer(direction);
                //transform.position += transform.forward * v * speed * Time.deltaTime;
                //transform.position += camcontrol.transform.right * h * speed * Time.deltaTime;
                transform.Rotate(transform.up * Time.deltaTime * speedRotF * x);
            }
            else
            {
                anim.SetBool("walk", false);
            }

        }

        #endregion

        
    }
    private GameObject GetNearestObject()
    {
        Debug.Log("buscando interaccion");
        Collider[] interactables = Physics.OverlapCapsule(basecapsule.position, heightcapsule.position, radiuscapsule, layermaskA);
        foreach (Collider interactable in interactables)
        {
            var newDist = Vector3.Distance(transform.position, interactable.gameObject.transform.position);
            if(newDist < actualDist)
            {
                actualDist = newDist;
                nearestCollider = interactable;
            }
        }
        var gameO = nearestCollider;
        if (gameO != null)
        {
            return gameO.gameObject;
        }
        return null;
    }
    private void CameraController()
    {
        float x = Input.GetAxis("Mouse X");
        if (camcontrol.endtransition)
        {
            
            if (!focus)
            {
                camcontrol.FreeMode(x);
            }
            else
            {   
                camcontrol.FocusMode(x);
            }
        }
        else
        {
            if (focus)
            {
                camcontrol.TransitionMode(focuscampos.position, focus);
            }
            else
            {
                camcontrol.TransitionMode(freecampos.position, focus);
            }
        }
        
    }

    void MovePlayer(Vector3 _dir)
    {
        rb.MovePosition(rb.position + _dir * speed * Time.fixedDeltaTime);
        //rb.AddForce(_dir * speed, ForceMode.Acceleration);
//        rb.velocity = _dir * speed * Time.deltaTime;
    }
    private void Attack()
    {
        instance_hitbox = Instantiate(hitbox, hitPos);
        instance_hitbox.GetComponent<hitbox>().au_manager = audiomanager;
    }
    public void Jumping()
    {
        rb.AddForce(Vector3.up * forcejump, ForceMode.Impulse);
    }
    protected override void Death()
    {
        if(lives > 0)
        {
            lives--;
            life.Life = 100;
            GameManager.instance.GoTolastCheckPoint();
            
        }
        else
        {
            SceneManager.LoadScene(3);
            Cursor.lockState = CursorLockMode.None;
        }
        
        

    }
    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        if(!defending) anim.SetTrigger("Hit");
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
        
        PlayerMovements();
        CameraController();
    }

    protected override void OnLateUpdate()
    {
        PlayerInputs();
    }
    #endregion
}
