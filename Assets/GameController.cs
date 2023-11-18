using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int numEnemies;
    public int numChests;

    void Start()
    {
        numEnemies = GameObject.FindGameObjectsWithTag("Rat").Length;
    }

    public void killEnemy()
    {
        numEnemies--;
        checkWin();
    }
    public void getChest()
    {
        numChests--;
        checkWin();
    }
    private void checkWin()
    {
        if (numEnemies == 0 && numChests == 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
    public void lose()
    {
        SceneManager.LoadScene("LossScreen");
    }
}
