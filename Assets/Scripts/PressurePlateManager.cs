using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    [SerializeField] private int numberOfKittensNeeded = 2;
    [SerializeField] private GameObject doorToOpen = null;
    [SerializeField] private Sprite pressedInButton = null;
    [SerializeField] private Sprite regularButton = null;

    private List<GameObject> kittens = new();

    private int currentKittens = 0;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = regularButton;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("kitten")){
            if(currentKittens != numberOfKittensNeeded){
                kittens.Add(other.gameObject);
                other.gameObject.SetActive(false);
                currentKittens++;
                if(currentKittens == 1){
                    GetComponent<SpriteRenderer>().sprite = pressedInButton;
                }
            }
            if(currentKittens == numberOfKittensNeeded){
                Debug.Log("All kittens loaded!");
                foreach(GameObject kitten in kittens){
                    kitten.SetActive(true);
                }
                doorToOpen.GetComponent<SpriteRenderer>().enabled = true;
                doorToOpen.GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = regularButton;
            }
        }
    }

}
