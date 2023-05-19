using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : LifeEntity
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CameraController camcontrol;
    [SerializeField] float forcejump = 2;
    [SerializeField] float speed = 5;
    [SerializeField] float speedRotF = 5;
    [SerializeField] float smoothnessrotation =5;
    [SerializeField] float angle;
    [SerializeField] GameObject hitbox;
    [SerializeField] Transform hitPos;

    public Transform focuscampos;
    public Transform freecampos;
    public Transform target;
    public Animator anim;

    public bool focus;
    public bool ontarget;

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
        float x = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("x", h);
        anim.SetFloat("y", v);

        //Movimiento
        #region

        if (!anim.GetBool("blocking"))
        {

            if((v != 0 || h != 0) && !focus)
            {

                anim.SetBool("walk", true);
                transform.position += camcontrol.transform.forward * v * speed * Time.deltaTime;
                transform.position += camcontrol.transform.right * h * speed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward,
                                            new Vector3(camcontrol.transform.forward.x, 0, camcontrol.transform.forward.z),
                                            smoothnessrotation * Time.deltaTime);
            }
            else if((v != 0 || h != 0 || x != 0) && focus)
            {
                anim.SetBool("walk", true);
                transform.position += transform.forward * v * speed * Time.deltaTime;
                transform.position += camcontrol.transform.right * h * speed * Time.deltaTime;
                transform.Rotate(transform.up * Time.deltaTime * speedRotF * x);
            }
            else
            {
                anim.SetBool("walk", false);
            }

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
            defending = true;
        }

        if (Input.GetButtonUp("Block"))
        {
            anim.SetBool("blocking", false);
            defending = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
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
                camcontrol.FocusMode(x);
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
    public void Jumping()
    {
        rb.AddForce(Vector3.up * forcejump, ForceMode.Impulse);
    }
    protected override void Death()
    {
        base.Death();
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.None;

    }
    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        anim.SetTrigger("Hit");
    }
}
