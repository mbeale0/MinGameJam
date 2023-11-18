using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    [SerializeField] private int numberOfKittensNeeded = 2;
    [SerializeField] private GameObject doorToOpen = null;
    [SerializeField] private GameObject button = null;

    private List<GameObject> kittens = new();

    private int currentKittens = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("kitten")){
            if(currentKittens != numberOfKittensNeeded){
                kittens.Add(other.gameObject);
                other.gameObject.SetActive(false);
                currentKittens++;
                if(currentKittens == 1){
                    button.transform.localScale = new Vector3(.8f, .8f, .8f);
                }
            }
            if(currentKittens == numberOfKittensNeeded){
                Debug.Log("All kittens loaded!");
                foreach(GameObject kitten in kittens){
                    kitten.SetActive(true);
                }
            }
        }
    }

}
