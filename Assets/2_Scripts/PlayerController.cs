using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LifeEntity
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CameraController camcontrol;
    [SerializeField] float forcejump = 2;
    [SerializeField] float speed = 5;
    [SerializeField] float smoothnessrotation =5;
    [SerializeField] float angle;
    [SerializeField] GameObject hitbox;
    [SerializeField] Transform hitPos;

    public Transform focuscampos;
    public Transform freecampos;
    public Transform target;
    public Animator anim;
    public bool focus;

    protected override void Awake()
    {
        base.Awake();
        camcontrol = GameManager.instance.camcontrol;
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        print(freecampos);
        camcontrol.InitCam(freecampos);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        PlayerInputs();
        CameraController();
    }
    private void PlayerInputs()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("x", h);
        anim.SetFloat("y", v);

        //Movimiento
        #region
        if ((v != 0 || h != 0) && !anim.GetBool("blocking"))
        {
            //direcciono el movimiento del player en base a donde mira la camara
            if (!focus)
            {
                transform.position += camcontrol.transform.forward * v * speed * Time.deltaTime;
                transform.position += camcontrol.transform.right * h * speed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward,
                                            new Vector3(camcontrol.transform.forward.x, 0, camcontrol.transform.forward.z),
                                            smoothnessrotation *Time.deltaTime);
            }
            else
            {
                transform.position += transform.forward * v * speed * Time.deltaTime;
                transform.RotateAround(target.transform.position, Vector3.up, h * smoothnessrotation * Time.deltaTime);
                transform.forward = (target.transform.position) - transform.position;
                
            }
            
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
        #endregion

        //Acciones
        #region 
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("punchB");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("punchA");
        }

        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("kick");
        }
        if (Input.GetButtonDown("Block"))
        {
            anim.SetBool("blocking", true);
        }

        if (Input.GetButtonUp("Block"))
        {
            anim.SetBool("blocking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
            rb.AddForce(Vector3.up * forcejump, ForceMode.Impulse);

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
                camcontrol.FocusMode();
            }
        }
        else
        {
            Debug.Log("transicion de camera");
            if (focus)
            {
                Debug.Log(" a free");
                camcontrol.TransitionMode(focuscampos.position, focus);
            }
            else
            {
                Debug.Log(" a focus");
                camcontrol.TransitionMode(freecampos.position, focus);
            }
        }
        
    }
    private void Attack()
    {
        Instantiate(hitbox, hitPos);
    }
}
