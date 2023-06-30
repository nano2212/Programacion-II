using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(EnemyManager.instance.enemies.Count <= 0)
        {
            if(other.gameObject.name == "Player")
            {
                SceneManager.LoadScene(7);
            }
        }
    }
}
