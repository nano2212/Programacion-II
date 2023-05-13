using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LiveEntity
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CameraController camcontrol;
    [SerializeField] float forcejump = 2;
    [SerializeField] float speed = 5;
    [SerializeField] float speedFocus = 2;
    [SerializeField] float smoothnessrotation =5;
    [SerializeField] float angle;

    public Transform focuscampos;
    public Transform freecampos;
    public Transform target;
    public Animator anim;

    public bool focus;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        camcontrol = GameManager.instance.camcontrol;
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
        CameraController();
    }
    private void PlayerInputs()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("x", h);
        anim.SetFloat("y", v);



        if ((v != 0 || h != 0) && !anim.GetBool("blocking"))
        {
            //direcciono el movimiento del player en base a donde mira la camara
            if (focus)
            {

                transform.position += camcontrol.transform.forward * v * speed * Time.deltaTime;
                transform.RotateAround(target.transform.position, Vector3.up, h * smoothnessrotation * Time.deltaTime);
                transform.forward = target.transform.position - transform.position;
                //transform.position += cam.transform.right * h * speedFocus * Time.deltaTime * percentSpeed;
                //transform.position += cam.transform.forward * v * speedFocus * Time.deltaTime;
            }
            else
            {

                transform.position += camcontrol.transform.forward * v * speed * Time.deltaTime;
                transform.position += camcontrol.transform.right * h * speed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward,
                                            new Vector3(camcontrol.transform.forward.x, 0, camcontrol.transform.forward.z),
                                            smoothnessrotation);
            }
            //transform.position += cam.transform.forward * v * speed * Time.deltaTime;
            //transform.position += cam.transform.right * h * speed * Time.deltaTime;
            //giro el personaje en direccion donde apunta la camara con un leve smooth


            //transform.position += transform.forward * v * speed * Time.deltaTime;
            //transform.position += transform.right * h * speed * Time.deltaTime;
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

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

        if (Input.GetButtonDown("FocusMode"))
        {

            if (!focus)
            {
                camcontrol.endtransition = true;
                anim.SetBool("focus", true);
                focus = true;
            }
            else
            {
                focus = false;
                anim.SetBool("focus", false);
            }
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
    }
    private void CameraController()
    {
        float x = Input.GetAxis("Mouse X");
        if (camcontrol.endtransition)
        {
            if (focus)
            {
                camcontrol.FocusMode();
            }
            else
            { 

                camcontrol.FreeMode(x);
            }
        }
        else
        {
            if (focus)
            {
                camcontrol.TransitionMode(freecampos.position - focuscampos.position);

            }
            else
            {
                camcontrol.TransitionMode(focuscampos.position);
            }
        }
        
    }
}
