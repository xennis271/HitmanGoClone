using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public GameObject MoveHereIcon;
    Vector2 lastClickedPoint;
    bool moving = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastClickedPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
            MoveHereIcon.SetActive(true);
            MoveHereIcon.GetComponent<Flip>().FlipStart();
        }
        if (moving && (Vector2)transform.position != lastClickedPoint) // if we are moving and not there
        {
            
            
            MoveHereIcon.transform.position = lastClickedPoint;
            MoveHereIcon.transform.position = new Vector3(lastClickedPoint.x, lastClickedPoint.y, -1); // so you can always see it
            float step = speed * Time.deltaTime; // makes the speed not linked to framerate
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPoint, step);
        }
        else
        {
            moving = false; // we are here!
            MoveHereIcon.SetActive(false);
        }
    }
}
