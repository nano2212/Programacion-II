using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEntity : MonoBehaviour
{
    LifeComponent life;
    [SerializeField] int maxLife;

    protected virtual void Death()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        life = new LifeComponent(maxLife, maxLife);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
