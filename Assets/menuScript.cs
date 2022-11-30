using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("loaded");
    }
    public void QuitGame()
    {
        Debug.Log("quit!!");
        Application.Quit();
    }
}
