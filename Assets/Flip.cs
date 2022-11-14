using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flip : MonoBehaviour
{
    public GameObject Icon;
    public Sprite FrameOne;
    public Sprite FrameTwo;
    // Start is called before the first frame update
    public void FlipStart()
    {
        StopAllCoroutines();
        StartCoroutine(Flipe());
    }

    private IEnumerator Flipe()
    {
        WaitForSeconds LoopForMe = new WaitForSeconds(1);
        while (true)
        {
            
                Icon.GetComponent<SpriteRenderer>().sprite = FrameTwo;
                //Debug.Log("2");
            
            yield return LoopForMe;
            
                Icon.GetComponent<SpriteRenderer>().sprite = FrameOne;
                //Debug.Log("1");

            yield return LoopForMe;
        }
        
    }
}
