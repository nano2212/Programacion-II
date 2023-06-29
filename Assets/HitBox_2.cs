using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_2 : MonoBehaviour
{
    int dmg = 5;
    public AudioManager au_manager = null;
    private void OnTriggerEnter(Collider other)
    {
        LifeEntity life = other.gameObject.GetComponent<LifeEntity>();
        AudioManager audiomanager = other.gameObject.GetComponent<AudioManager>();
        if (life)
        {
            au_manager.PlaySound("hit");
            if(!life.defending) audiomanager.PlaySound("damage");
            life.TakeDamage(dmg);
        }
    }
}
