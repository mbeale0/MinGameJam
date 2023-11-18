using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kittenMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int state;
    // State:
    //0: following alive
    //1: returning alive
    //2: thrown
    //3: following dead
    //4: returning dead

    public GameObject cat;
    public float followDistance;
    public float followSpeed;
    public float throwSpeed;
    public float returnSpeed;
    public float throwTime;
    private float timer;
    private Vector3 throwDir;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vecToCat;
        switch (state)
        {
            case 0:
            case 3: // follow
                vecToCat = cat.transform.position - transform.position;
                if (Vector3.Magnitude(vecToCat) > followDistance + state/3)
                {
                    transform.position += (followSpeed/60 * Vector3.Normalize(vecToCat));
                }
                return;
            case 2: // throw
                if (timer > 0)
                {
                    transform.position += (throwSpeed / 60 * throwDir);
                    timer -= Time.deltaTime;
                } else
                {
                    state = 1;
                }
                return;
            case 1:
            case 4: // return
                vecToCat = cat.transform.position - transform.position;
                transform.position += (returnSpeed / 60 * Vector3.Normalize(vecToCat));
                if (Vector3.Magnitude(vecToCat) < followDistance/2)
                {
                    state--;
                }
                    return;
            default:
                Debug.LogError("Invalid state: " + state);
                return;
        }
    }

    public void ThrowKitten()
    {
        timer = throwTime;
        transform.position = cat.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = cat.transform.position.z;
        throwDir = Vector3.Normalize(mousePos - cat.transform.position);
        state = 2;
    }
}
