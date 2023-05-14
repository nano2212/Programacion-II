using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    int dmg = 5;
    [SerializeField] float timeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeEntity life = other.gameObject.GetComponent<LifeEntity>();

        if (life)
        {
            Debug.Log(other.gameObject.name);
            Debug.Log("golpea");
            life.TakeDamage(dmg);
        }
        Destroy(this.gameObject);
    }
}
