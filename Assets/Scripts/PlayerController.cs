using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject[] kittens = new GameObject[5];
    public GameObject kitten;

    private void Start()
    {
        kittens[0] = kitten;
        for (int i = 1; i <= 4; i++)
        {
            kittens[i] = Instantiate(kitten, transform.position + new Vector3(Random.Range(-1,1), Random.Range(-1, 1), transform.position.z), Quaternion.identity);
        }

    }

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

        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject k in kittens)
            {
                if (k.GetComponent<kittenMovement>().state == 0)
                {
                    k.GetComponent<kittenMovement>().ThrowKitten();
                    break;
                }
            }
        }
    }

    

    
}
