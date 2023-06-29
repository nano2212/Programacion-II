using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject playerGO;
    public CameraController camcontrol;
    public Transform camRef;
    public bool nextLevel = false;
    public static GameManager instance;

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

}
