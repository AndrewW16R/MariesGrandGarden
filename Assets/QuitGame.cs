using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
