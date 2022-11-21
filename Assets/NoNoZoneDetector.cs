using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoNoZoneDetector : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // WE HIT SOMETHING!
        Debug.Log("Hit! :" + collision.gameObject.name.ToString());
    }
}
