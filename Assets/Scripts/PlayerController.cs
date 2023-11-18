using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite walkRight = null;
    [SerializeField] private Sprite walkLeft = null;
    [SerializeField] private Sprite standing = null;
    private GameObject[] kittens = new GameObject[5];
    public GameObject kitten;
    private bool isMoving = false;
    private bool inWalkingAnimation = false;

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
        if(isMoving && !inWalkingAnimation){
            inWalkingAnimation = true;
            StartCoroutine(AnimateWalking());
        }
        if(Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            transform.Translate(0, 10 * Time.deltaTime, 0);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            isMoving = true;
            transform.Translate(0, -10 * Time.deltaTime, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            transform.Translate(-10 * Time.deltaTime, 0, 0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            transform.Translate(10 * Time.deltaTime, 0, 0);
        }
        else{
            isMoving = false;
            GetComponent<SpriteRenderer>().sprite = standing;
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
    public void takeDamage()
    {
        //TODO: Lose
    }   

    private IEnumerator AnimateWalking(){
        GetComponent<SpriteRenderer>().sprite = walkRight;
        yield return new WaitForSeconds(.2f);
        GetComponent<SpriteRenderer>().sprite = walkLeft;
        yield return new WaitForSeconds(.2f);
        inWalkingAnimation = false;
    }
    
}
