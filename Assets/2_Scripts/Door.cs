using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : PlayObject, Iinteractable
{
    bool IsOpen = false;
    bool IsMoving = false;
    
    [SerializeField] float speed;
    [SerializeField] Transform door;
    [SerializeField] Transform openPos;
    [SerializeField] Transform closePos;
    [SerializeField] Vector3 targetPos;
    public void Interact()
    {
        Debug.Log("Interactuaste con la puerta ");
        if (!IsOpen)
        {
            targetPos = openPos.position;
            Debug.Log(" y estaba cerrada");
        }
        else
        {   
            targetPos = closePos.position;
            Debug.Log(" y estaba abierta");
        }
        IsOpen = !IsOpen;
        IsMoving = true;
    }
    private void Move(Vector3 targetPosition)
    {
        Vector3 dir = (targetPosition - door.position).normalized;
        door.position += dir * speed * Time.deltaTime;
    }


    protected override void OnFixedUpdate()
    {
        if (!IsMoving) return;
        if (Vector3.Distance(door.position, targetPos) >= 0) 
        {
            Move(targetPos);
            Debug.Log("me estoy moviendo");
        }
        else
        {
            Debug.Log("no me estoy moviendo");
            IsMoving = false;
        }
    }
    //Funciones clase abstracta PlayObject sin usar
    #region
    protected override void OnDeInitialize()
    {
    }
    
    protected override void OnInitialize()
    {
    }
    protected override void OnPause()
    {
    }
    protected override void OnResume()
    {
    }
    protected override void OnUpdate()
    {

    }

    protected override void OnLateUpdate()
    {
    }
    #endregion
}
