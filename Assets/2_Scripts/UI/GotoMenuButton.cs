using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoMenuButton : MonoBehaviour
{
   

    // Update is called once per frame
    public void ButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
