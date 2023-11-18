using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(Keycode.W)){
            transform.Translate(0, 1, 0);    
        }     
    }

    

    
}
