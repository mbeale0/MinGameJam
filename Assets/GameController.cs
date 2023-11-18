using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int numEnemies;
    public int numChests;

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
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        }
    }
    public void lose()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(2));
    }
}
