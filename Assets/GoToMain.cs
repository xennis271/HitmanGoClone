using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Ran C");
        StartCoroutine(GoToMainC());
    }
    IEnumerator GoToMainC()
    {
        //Debug.Log("Waiting one sec to move to main");
        
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene("main menu");
    }
    
}
