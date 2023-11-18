using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainofMilk : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kitten"))
        {
            if (other.GetComponent<kittenMovement>().state >= 3)
            {
                other.GetComponent<kittenMovement>().state = 1;
            }
        }
    }
}
