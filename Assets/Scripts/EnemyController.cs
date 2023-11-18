using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject attackRadius = null;
    [SerializeField] private GameObject smoke = null;
    private float speed = 3.5f;
    private Transform target = null;
    private bool attacking = false;
    private int numberOfKittensAttacking = 0;
    private bool isBeingAttacked = false;
    private float health = 15;
    private bool isInAttackCoroutine = false;
    private List<GameObject> kittens = new();
    void Start()
    {
       target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    void Update()
    {
        if(isBeingAttacked && !isInAttackCoroutine){
            StartCoroutine(AttackMode());
        }
        if(!isBeingAttacked && attackRadius.GetComponent<AttackManager>().getTargetInRadius()){
            attacking = true;
            StopAllCoroutines();
            StartCoroutine(ChaseCountDown());
        }
        if(!isBeingAttacked && attacking){

            //rotate to look at the player
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0,-90,0),Space.Self);
                        
            //move towards the player
            if (Vector3.Distance(transform.position,target.position)>2f){
                transform.Translate(new Vector3(speed* Time.deltaTime,0,0) );
            }
        }
    }
    private IEnumerator AttackMode(){
        isInAttackCoroutine = true;
        yield return new WaitForSeconds(.75f);
        List<GameObject> kittensToKill = new();
        foreach(GameObject kitten in kittens){
            health -= Random.Range(2.5f, 3.5f);
            if(health <= 0){
                Destroy(gameObject);
            }
            kitten.GetComponent<KittenHealth>().SetHealth(kitten.GetComponent<KittenHealth>().GetHealth() - Random.Range(1.5f, 3.6f));
            if(kitten.GetComponent<KittenHealth>().GetHealth() <= 0){
                kitten.gameObject.SetActive(false);
                kittensToKill.Add(kitten);                
            }        
        }
        foreach(GameObject kitten in kittensToKill){
            kittens.Remove(kitten);
            numberOfKittensAttacking--;
        }
        if(kittens.Count == 0){
            isBeingAttacked = false;
            smoke.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        isInAttackCoroutine = false;
    }

    private IEnumerator ChaseCountDown(){
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Kitten")){
            isBeingAttacked = true;
            numberOfKittensAttacking++;
            kittens.Add(other.gameObject);
            // TODO: Disable kitten controller
            other.gameObject.transform.position = gameObject.transform.position;
            if(numberOfKittensAttacking == 1){
                smoke.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

    }
}