using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerPivot;
    [SerializeField] PlayerController playerC;
    public float speedRotation;
    public float smoothtransition;
    public float smoothrotationn;
    public Transform normalcam;
    public bool endtransition = true;
    
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        playerPivot = GameManager.instance.playerTransform;
        playerC = GameManager.instance.player;
    }

    public void FocusMode()
    {
        //transform.position = playerC.fcpos.position + new Vector3(0, .5f, 0);
        transform.position = Vector3.Lerp(transform.position, 
                                        playerC.focuscampos.position + new Vector3(0, .5f, 0), 
                                        smoothrotationn * Time.deltaTime); ;
        transform.forward = playerPivot.forward;
    }
    public void FreeMode(float mouseX)
    {
        //transform.position = Vector3.Lerp(transform.position,
        //                                playerC.freecampos.position, mouseX *
        //                                smoothrotationn * Time.deltaTime); ;
        //transform.forward = playerPivot.position - transform.position;
        transform.RotateAround(playerPivot.position, Vector3.up, speedRotation * mouseX * Time.deltaTime);
    }
    public bool TransitionMode(Vector3 target)
    {
        if(transform.position == target)
        {
            transform.position = Vector3.Lerp(transform.position, target, smoothtransition * Time.deltaTime);
            endtransition = true;
        }
        return endtransition;
    }

}
