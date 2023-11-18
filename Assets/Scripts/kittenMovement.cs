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
    private bool isWalking = false;
    private bool inWalkingAnimation = false;
    void Update()
    {
        if(isWalking && !inWalkingAnimation){
            inWalkingAnimation = true;
            StartCoroutine(AnimateWalking());
        }
        Vector3 vecToCat;
        switch (state)
        {
            case 0:
            case 3: // follow
                isWalking = true;
                vecToCat = cat.transform.position - transform.position;
                if (Vector3.Magnitude(vecToCat) > followDistance + state/3)
                {
                    transform.position += (followSpeed/60 * Vector3.Normalize(vecToCat));
                }
                return;
            case 2: // throw
                isWalking = false;
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
                isWalking = true;
                if (Vector3.Magnitude(vecToCat) < followDistance/2)
                {
                    state--;
                }
                    return;
            default:
                isWalking = false;
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
    private IEnumerator AnimateWalking(){
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, .35f, 1);
        yield return new WaitForSeconds(.2f);
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, .5f, 1);
        yield return new WaitForSeconds(.2f);
        inWalkingAnimation = false;
    }
}
