using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhereAreYouGoing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {

            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(4);
        }
        
    }
}
