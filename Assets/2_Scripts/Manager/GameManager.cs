using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject playerGO;
    public CameraController camcontrol;
    public Transform camRef;
    public bool nextLevel = false;
    public static GameManager instance;
    public Vector3 lastCheckPoint;
    public Animator UiAnimator;

    public void Awake()
    {
        if( instance != null)
        {
            Destroy(this.gameObject);
            
        }
        else
        {
            instance = this;
        }
        
    }
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GoTolastCheckPoint()
    {
        playerGO.transform.position = lastCheckPoint;
    }
    
}
