using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerController playerRef;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
