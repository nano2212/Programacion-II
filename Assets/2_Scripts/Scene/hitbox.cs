using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    int dmg = 5;
    public AudioManager au_manager = null;
    [SerializeField] float timeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeEntity life = other.gameObject.GetComponent<LifeEntity>();
        AudioManager audiomanager = other.gameObject.GetComponent<AudioManager>();
        
        if (life)
        {
            au_manager.PlaySound("hit");
            audiomanager.PlaySound("damage");
            life.TakeDamage(dmg);
        }
        Destroy(this.gameObject);
    }
}
