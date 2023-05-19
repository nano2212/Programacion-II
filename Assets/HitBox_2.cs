using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox_2 : MonoBehaviour
{
    int dmg = 5;

    private void OnTriggerEnter(Collider other)
    {
        LifeEntity life = other.gameObject.GetComponent<LifeEntity>();

        if (life)
        {
            Debug.Log(other.gameObject.name);
            life.TakeDamage(dmg);
        }
    }
}
