using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject attackRadius = null;
    private float speed = 3.5f;
    private Transform target = null;
    private bool attacking = false;
    void Start()
    {
       target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    void Update()
    {
        if(attackRadius.GetComponent<AttackManager>().getTargetInRadius()){
            attacking = true;
            StopAllCoroutines();
            StartCoroutine(ChaseCountDown());
        }
        if(attacking){

            //rotate to look at the player
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0,-90,0),Space.Self);
            
            
            //move towards the player
            if (Vector3.Distance(transform.position,target.position)>2f){
                transform.Translate(new Vector3(speed* Time.deltaTime,0,0) );
            }
        }
    }
    private IEnumerator ChaseCountDown(){
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

}
