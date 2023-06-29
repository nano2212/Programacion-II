using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<EnemyBase> enemies;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            
        }
        else
        {
            instance = this;
        }
    }   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count == 0)
        {
            GameManager.instance.nextLevel = true;
        }
    }
}
