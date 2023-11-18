using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private bool isTargetInRadius = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            isTargetInRadius = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            isTargetInRadius = false;
        }
    }

    public bool getTargetInRadius(){
        return isTargetInRadius;
    }
}
