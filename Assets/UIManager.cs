using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text SlidingText;
    public Vector3 target;
    
    float t;

    // Start is called before the first frame update
    void Start()
    {
        // Starting UI animation
        print("[UIManager]: SlideOnScreen Starting");
        Vector3 Offset = new Vector3(SlidingText.rectTransform.position.x - 500, SlidingText.rectTransform.position.y, SlidingText.rectTransform.position.z);
        target = SlidingText.rectTransform.position;
        SlidingText.rectTransform.position = Offset;

    }



    // Update is called once per frame
    void Update()
    {
        // X - 500
        t += Time.deltaTime / 5.0f;
        
        
        SlidingText.rectTransform.position = Vector3.Lerp(SlidingText.rectTransform.position, target, t);
    }
}
