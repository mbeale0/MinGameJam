using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainofMilk : MonoBehaviour
{
    [SerializeField] private Sprite kat = null;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("kitten"))
        {
            if (other.GetComponent<kittenMovement>().state >= 3)
            {
                other.GetComponent<SpriteRenderer>().sprite = kat;
                other.GetComponent<SpriteRenderer>().enabled = true;
                other.GetComponent<kittenMovement>().state = 1;
            }
        }
    }
}
