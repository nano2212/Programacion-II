using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Transform playerPivot;
    public Transform _camRef;
    [SerializeField] PlayerController playerC;
    public Transform normalcam;
    Camera cameraObj;

    [Header("Velocidades")]
    public float speedRotation;
    public float smoothtransition;
    public float smoothrotationn;

    [Header("Variables de transicion de camara")]
    [SerializeField] float freefield;
    [SerializeField] float focusfield;
    [SerializeField] float radspherecast;
    public LayerMask layermask;
    public RaycastHit hitinfo;
    float distcams;

    [Header("Booleanos")]
    public bool endtransition = true;

    [Header("Target vars")]
    public bool ontarget;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPivot = GameManager.instance.player.transform;
        playerC = GameManager.instance.player;
        _camRef = GameManager.instance.camRef;
        cameraObj = GetComponent<Camera>();
        distcams = Vector3.Distance(playerC.focuscampos.position, playerC.freecampos.position);
    }

    public void FocusMode(float mouseX)
    {
        
        transform.position = playerC.focuscampos.position;
        transform.forward = playerPivot.forward;
        
    }
    //public void FocusMode(float mouseX, bool ontarget)
    //{
    //    if (ontarget)
    //    {
    //        transform.position = Vector3.Lerp(transform.position,
    //                                    playerC.focuscampos.position + new Vector3(0, .5f, 0),
    //                                    smoothrotationn * Time.deltaTime);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, playerPivot.rotation, smoothrotationn * Time.deltaTime);
    //        transform.Rotate(transform.up * Time.deltaTime * speedRotation * mouseX);
    //    }
    //    else
    //    {
    //        transform.position = playerC.focuscampos.position;
    //        transform.forward = playerPivot.forward;
    //    }
        
    //}
    public void FreeMode(float mouseX)
    {
        transform.RotateAround(playerPivot.position, Vector3.up, speedRotation * mouseX * Time.deltaTime);
    }
    public void TransitionMode(Vector3 target, bool focus)
    {        
        if (focus)
        {
            cameraObj.fieldOfView = focusfield + (freefield - focusfield) * Vector3.Distance(transform.position, target) / distcams;
            transform.parent = null;
            
        }
        else
        {
            cameraObj.fieldOfView = freefield + (focusfield - freefield) * Vector3.Distance(transform.position, target) / distcams;
            transform.parent = _camRef;
        }
        if (transform.position != target)
        {

            transform.position = Vector3.Lerp(transform.position, target, smoothtransition * Time.deltaTime);
        }
        //if (!Physics.SphereCast(target, radspherecast, Vector3.back, out hitinfo, 0, layermask))
        //{
        //    print("sigo aca");
        //    transform.position = Vector3.Lerp(transform.position, target, smoothtransition * Time.deltaTime);
        //}
        else
        {
            
            print("Transición completa");
            endtransition = true;
        }

    }

    public void InitCam(Transform posinit)
    {
        transform.position = posinit.position;
        transform.parent = GameManager.instance.camRef;

    }
}
