using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 10 * Time.deltaTime, 0);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -10 * Time.deltaTime, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(-10 * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(10 * Time.deltaTime, 0, 0);
        }
    }

    

    
}
