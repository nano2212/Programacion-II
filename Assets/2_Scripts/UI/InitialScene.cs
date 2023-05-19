using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InitialScene : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ButtonPressed()
    {
        SceneManager.LoadScene(5);
    }
    
}
