using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(0, 1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(0, -1, 0);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(-1, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(1, 0, 0);
        }
    }

    

    
}
