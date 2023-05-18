using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public CameraController camcontrol;
    public Transform camRef;
    
    List<Enemy> enemies;     // Start is called before the first frame update
    List<Interactable> interactable;

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
