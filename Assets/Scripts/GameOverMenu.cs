using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Anropas när vi tycker på "New Game" knappen i Game Over scenen.
    public void NewGame()
    {
        SceneManager.LoadScene("Level");
    }
}
