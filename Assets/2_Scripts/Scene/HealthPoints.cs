using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : PlayObject, Iinteractable
{
    [SerializeField] int healthpoints;
    LifeEntity life;
    void Start()
    {
        life = GameManager.instance.playerGO.GetComponent<LifeEntity>();
    }
    public void Interact()
    {
        Debug.Log("Oh he subido 10 de vida");
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            life.Heal(healthpoints);
            Destroy(this.gameObject);
        }
    }
    //Funciones clase abstracta PlayObject
    #region
    protected override void OnDeInitialize()
    {
    }
    protected override void OnFixedUpdate()
    {
    }
    protected override void OnLateUpdate()
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
    #endregion
}
