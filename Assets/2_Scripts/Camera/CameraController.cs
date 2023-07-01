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
    float distcams;
    [SerializeField] float tolerancia = 0.3f;


    [Header("Booleanos")]
    public bool endtransition = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _camRef = GameManager.instance.camRef;
        playerPivot = GameManager.instance.player.transform;
        playerC = GameManager.instance.player;
        
        cameraObj = GetComponent<Camera>();
        distcams = Vector3.Distance(playerC.focuscampos.position, playerC.freecampos.position);
    }

    public void FocusMode(float mouseX)
    {
        
        transform.position = playerC.focuscampos.position;
        transform.forward = playerPivot.forward;
        
    }
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
        var dist = Vector3.Distance(transform.position, target);
        if (dist >= tolerancia)
        {
            transform.position = Vector3.Lerp(transform.position, target, smoothtransition * Time.deltaTime);
        }
        else
        {
            endtransition = true;
        }

    }

    public void InitCam(Transform posinit)
    {
        transform.position = posinit.position;
        transform.parent = GameManager.instance.camRef;

    }
}
