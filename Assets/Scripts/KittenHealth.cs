using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenHealth : MonoBehaviour
{
    private float health = 7f;

    public void ResetHealth(){
        health = 7f;
    }
    public void SetHealth(float newHealth){
        health = newHealth;
    }
    public float GetHealth(){
        return health;
    }
}
