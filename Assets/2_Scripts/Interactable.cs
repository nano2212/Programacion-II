using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Permite que el objeto que tenga este componente sea interactuable con el jugador
    [SerializeField] int healthpoints;
    [SerializeField] PlayerController playerRef;

    public void Start()
    {
        playerRef = GameManager.instance.player;
    }
    

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        LifeEntity life = other.gameObject.GetComponent<LifeEntity>();
        string namecol = other.gameObject.name;
        if (life != null && namecol == "Player")
        {
            life.Heal(healthpoints);
            Destroy(this.gameObject);
        }
    }
}
