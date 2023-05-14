using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSmooth : MonoBehaviour
{
    [SerializeField] Transform playerTrans;
    public float smoothness = 5;
    public Vector3 offset;

    public void Start()
    {
        playerTrans = GameManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, playerTrans.position + offset, smoothness * Time.deltaTime);
    }
}
