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

    [Header("Velocidades")]
    public float speedRotation;
    public float smoothtransition;
    public float smoothrotationn;

    [Header("Booleanos")]
    public bool endtransition = true;
    // Start is called before the first frame update
    void Start()
    {
        playerPivot = GameManager.instance.player.transform;
        playerC = GameManager.instance.player;
        _camRef = GameManager.instance.camRef;
        
    }

    public void FocusMode()
    {
        transform.position = Vector3.Lerp(transform.position, 
                                        playerC.focuscampos.position + new Vector3(0, .5f, 0), 
                                        smoothrotationn * Time.deltaTime); ;
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
            transform.parent = null;
        }
        else
        {
            transform.parent = _camRef;
        }
        if (transform.position != target)
        {
            transform.position = Vector3.Lerp(transform.position, target, smoothtransition * Time.deltaTime);
        }
        else
        {
            print("Transición completa");
            endtransition = true;
        }
        
    }

    public void InitCam(Transform posinit)
    {
        print(posinit.position);
        transform.position = posinit.position;
        transform.parent = GameManager.instance.camRef;

    }
}
