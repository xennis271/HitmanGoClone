using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool OldMovement = false;
    public float speed = 10f;
    public GameObject MoveHereIcon;
    public GameObject Player;
    public GameObject ScriptBox;
    Vector2 lastClickedPoint;
    bool moving = false;
    public int PlayerScore = 0;
    Vector3 OldPos;

    // Update is called once per frame
    void Update()
    {
        if (OldMovement)
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
        else
        {
            // NEW MOVEMENT
            if (Input.GetMouseButtonDown(0))
            {
                OldPos = Player.GetComponent<Rigidbody2D>().position;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit)
                {
                    if (hit.collider.name == "Left" ||
                    hit.collider.name == "Right" ||
                    hit.collider.name == "Up" ||
                    hit.collider.name == "Down")
                    {
                        // we hit a movement orb!
                        //Debug.Log("Player hit! :" + hit.collider.name.ToString());
                        Vector3 NewPos;
                        switch (hit.collider.name.ToString())
                        {
                            case "Left":

                                NewPos = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.25f);
                                Player.GetComponent<Rigidbody2D>().MovePosition(NewPos);
                                break;
                            case "Right":
                                NewPos = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.25f);
                                Player.GetComponent<Rigidbody2D>().MovePosition(NewPos);
                                break;
                            case "Up":
                                NewPos = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.25f);
                                Player.GetComponent<Rigidbody2D>().MovePosition(NewPos);
                                break;
                            case "Down":
                                NewPos = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.25f);
                                Player.GetComponent<Rigidbody2D>().MovePosition(NewPos);
                                break;
                        }
                    }
                
                }

                // Player MOVED!
                ScriptBox.GetComponent<AIControler>().MoveAI();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // WE HIT SOMETHING!
        Debug.Log("Hit! :" + collision.gameObject.name.ToString());

        if(collision.gameObject.name == "NoNoZone" || collision.gameObject.name == "Tilemap" || collision.gameObject.name == "B1")
        {
            // uh oh that's a no no zone
            Player.GetComponent<Rigidbody2D>().MovePosition(OldPos);
            Player.GetComponent<Rigidbody2D>().rotation = 0f;
        }
        if (collision.gameObject.name.ToString().Equals("Coin"))
        {
            PlayerScore += 10; // for a coin
            Debug.Log("Score+:" + PlayerScore);
            Destroy(collision.gameObject);
            Player.transform.position = OldPos;
        }
        if (collision.gameObject.name.ToString().Equals("Treasure"))
        {
            PlayerScore += 100; // for a end
            Debug.Log("Score+:" + PlayerScore);
            Destroy(collision.gameObject);
            Player.transform.position = OldPos;
        }
    }
}

