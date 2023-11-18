using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject attackRadius = null;
    [SerializeField] private Sprite smoke_0 = null;
    [SerializeField] private Sprite smoke_1 = null;
    [SerializeField] private Sprite rat = null;
    [SerializeField] private Sprite ghost = null;
    
    public GameObject fireball;
    public float speed;
    public bool isRobot;
    public float fireballTime;
    private float fireballTimer;
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
        if (isBeingAttacked && !isInAttackCoroutine)
        {
            StartCoroutine(AttackMode());
        }
        if (!isBeingAttacked && attackRadius.GetComponent<AttackManager>().getTargetInRadius())
        {
            attacking = true;
            StopAllCoroutines();
            StartCoroutine(ChaseCountDown());
        }
        if (!isBeingAttacked && attacking)
        {
            //rotate to look at the player
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            //move towards the player
            if (Vector3.Distance(transform.position, target.position) > 2f)
            {
                fireballTimer -= Time.deltaTime;
                if (isRobot && fireballTimer < 0)
                {
                    Instantiate(fireball, transform.position, transform.rotation);
                    fireballTimer = fireballTime;
                }
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
        }
    }
    private IEnumerator AttackMode()
    {
        isInAttackCoroutine = true;
        GetComponent<SpriteRenderer>().sprite = smoke_0;
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().sprite = smoke_1;
        yield return new WaitForSeconds(.5f);
        List<GameObject> kittensToKill = new();
        foreach (GameObject kitten in kittens)
        {
            health -= Random.Range(2.5f, 3.5f);
            if (health <= 0)
            {
                foreach (GameObject victoryKitten in kittens)
                {
                    victoryKitten.GetComponent<kittenMovement>().state = 1;
                    victoryKitten.GetComponent<kittenMovement>().enabled = true;
                    victoryKitten.GetComponent<SpriteRenderer>().enabled = true;
                }
                Destroy(gameObject);
            }
            kitten.GetComponent<KittenHealth>().SetHealth(kitten.GetComponent<KittenHealth>().GetHealth() - Random.Range(1.5f, 3.6f));
            if (kitten.GetComponent<KittenHealth>().GetHealth() <= 0)
            {                
                kittensToKill.Add(kitten);
            }
        }
        foreach (GameObject kitten in kittensToKill)
        {
            kitten.GetComponent<kittenMovement>().state = 4;
            kitten.GetComponent<kittenMovement>().enabled = true;
            kitten.GetComponent<SpriteRenderer>().sprite = ghost;
            kitten.GetComponent<SpriteRenderer>().enabled = true;
            kittens.Remove(kitten);
            numberOfKittensAttacking--;
        }
        if (kittens.Count == 0)
        {
            isBeingAttacked = false;
            GetComponent<SpriteRenderer>().sprite = rat;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        isInAttackCoroutine = false;
    }

    private IEnumerator ChaseCountDown()
    {
        yield return new WaitForSeconds(2f);
        attacking = false;
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("kitten"))
        {
            isBeingAttacked = true;
            numberOfKittensAttacking++;
            kittens.Add(other.gameObject);
            // TODO: Disable kitten controller
            other.gameObject.GetComponent<kittenMovement>().enabled = false;
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.transform.position = gameObject.transform.position;
            if (numberOfKittensAttacking == 1)
            {
                GetComponent<SpriteRenderer>().sprite = smoke_0;
            }
        }

        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().takeDamage();
        }

    }
}
