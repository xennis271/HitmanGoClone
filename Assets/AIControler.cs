using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControler : MonoBehaviour
{
    public GameObject[] BasicAI;
    public GameObject[] FireAI;
    public static int MAXPROJECTILES = 10;
    public GameObject[] Projectiles;
    public Dictionary<GameObject, List<Vector3>> BasicAIMoves = new Dictionary<GameObject, List<Vector3>>();
    public GameObject Player;
    public Vector3 DebugRay = new Vector3(0, 0, 0);
    public Vector3 DebugRay2 = new Vector3(0, 0, 0);
    public GameObject Fire;
    public int BadguyViewDist = 1;
    public int FireAIchance = 25;
    public int FireAIDelay = 5;
    public void Start()
    {
        foreach (GameObject AI in BasicAI)
        {
            List<GameObject> AllChildren = new List<GameObject>();


            // first off let's get the Gos of all kids
            foreach (Transform child in AI.transform)
            {
                AllChildren.Add(child.gameObject);
            }
            // we have all the kids kick them to get the correct pos in realtion to the world
            AI.transform.DetachChildren();
            foreach (GameObject child in AllChildren)
            {
                if(child.name.ToString().Equals("Blocker"))
                    child.transform.parent = AI.transform;
            }
            List<Vector3> MovePoints = new List<Vector3>();
            // add self so we have the starting point
            MovePoints.Add(AI.transform.position);
            foreach (GameObject node in AllChildren)
            {
                if (!node.name.ToString().Equals("Blocker"))
                {
                    //Debug.Log("Node:" + int.Parse(node.name));
                    MovePoints.Insert(int.Parse(node.name) + 1, node.transform.position);
                    // remove the node after
                    GameObject.Destroy(node);
                }
            }
            // put this in the mega list
            BasicAIMoves.Add(AI, MovePoints);
            //MovePoints.Clear();
            //AllChildren.Clear();
        }
        // FireAI
        Projectiles = new GameObject[MAXPROJECTILES];
        //Debug.Log(Projectiles.Length);
        for (int i = 0; i < MAXPROJECTILES; i++)
        {
            //Debug.Log(i);
            GameObject Fireball = GameObject.Instantiate(Fire);
            Projectiles[i] = Fireball;
            Projectiles[i].transform.position = new Vector3(-999, -999, 999);
            Projectiles[i].name = (FireAIDelay+1).ToString();
        }
        foreach (GameObject AI in BasicAI)
        {
            //AI.transform.localScale = new Vector3(2, 2, 1);
        }
    }



    public int Step = 1;
    public void MoveAI()
    {

        foreach (GameObject AI in BasicAI)
        {
            // move to the correct spot.
            //Debug.Log(Step+1 + ">" + BasicAIMoves[AI].Count);
            if (Step >= BasicAIMoves[AI].Count)
            {
                // fewer steps than the count so just keep a fake step
                int FakeStep = Step;
                while (FakeStep >= BasicAIMoves[AI].Count)
                {
                    FakeStep -= BasicAIMoves[AI].Count;
                }
                //FakeStep += 1;
                //FakeStep = BasicAIMoves[AI].Count - FakeStep;
                //if (FakeStep < 0)
                //    FakeStep = 0;
                //Debug.Log("FakeStep:" + FakeStep);
                AI.transform.position = BasicAIMoves[AI][FakeStep];
            }
            else
            {
                //Debug.Log("Step:" + Step);
                AI.transform.position = BasicAIMoves[AI][Step];
            }









            // DETECTING SCRIPT
            foreach (Transform node in Player.transform.GetChild(1).transform)
            {
                node.GetComponent<CircleCollider2D>().enabled = false;
            }

            //AI.transform.GetChild(0).position = new Vector3(AI.transform.GetChild(0).position.x, AI.transform.GetChild(0).position.y, 0f);
            Vector3 oldTrans = AI.transform.position;
            //AI.transform.GetChild(0).transform.rotation = new Quaternion(0, -180, 0, 0);
            //AI.transform.GetChild(0).transform.LookAt(Player.transform);
            //AI.transform.GetChild(0).transform.rotation.Set(0,AI.transform.GetChild(0).transform.rotation.y-90f,AI.transform.GetChild(0).transform.rotation.z + 85f,0);
            AI.transform.LookAt(Player.transform);
            AI.transform.position = new Vector3(AI.transform.position.x,AI.transform.position.y,0);
            Transform LookingAtPlayerTrans = AI.transform;
            DebugRay = AI.transform.forward * BadguyViewDist;
            DebugRay2 = AI.transform.position;

            RaycastHit2D hit = Physics2D.Raycast(DebugRay2, DebugRay, BadguyViewDist);
            
            
            if (hit.collider != null)
            {
                
                if (hit.collider.gameObject.name.ToString().Equals("Player"))
                {
                    //Debug.Log("DebugRay:(" + DebugRay.x + "," + DebugRay.y + "," + DebugRay.z + ")");
                    //Debug.Log(((AI.transform.rotation.eulerAngles.x * AI.transform.rotation.x)/2)*-1 + ">" + " 30");
                    //Debug.Log("Dist:" + hit.distance);
                    if(hit.distance < 1.35f)
                    {
                        Debug.DrawRay(DebugRay2, DebugRay, Color.green, 3.0f);
                        //Debug.Log(AI.transform.gameObject.name.ToString() + " Hit legaly:" + hit.collider.gameObject.name.ToString());
                        Debug.Log("Player was caught GAME OVER");
                        Player.transform.DetachChildren();
                        Destroy(Player);
                    }
                    else
                    {
                        Debug.DrawRay(DebugRay2, DebugRay, Color.red, 3.0f);
                        //Debug.Log(AI.transform.gameObject.name.ToString() + " Hit wrongly:" + hit.collider.gameObject.name.ToString());
                    }
                    
                }
                    
                if (hit.collider.gameObject.name.ToString().Equals("Blocker"))
                    Debug.DrawRay(DebugRay2, DebugRay, Color.blue, 2.0f);
                
            }
            else
            {
                Debug.Log("Hit nothing");
            }



            AI.transform.position = oldTrans;
            AI.transform.rotation = new Quaternion();
            //Debug.DrawRay(DebugRay2, DebugRay, Color.green);

            foreach (Transform node in Player.transform.GetChild(1).transform)
            {
                node.GetComponent<CircleCollider2D>().enabled = true;
            }
           // Debug.Log("Pointing at player");
            
        }



        // Fireball AI


        // Create Fireball
        foreach (GameObject AI in FireAI)
        {
            // we fire on a % chance
            int outcome = Random.Range(1, 100);
            if(outcome <= FireAIchance)
            {
                // spawn fire on player
                //Debug.Log("AI fired");
                GameObject Fireball = GameObject.Instantiate(Fire);
                Fireball.name = FireAIDelay.ToString();
                bool SpawnedFire = false;
                for (int i = 0; i < Projectiles.Length; i++)
                {
                    if(Projectiles[i].transform.position.Equals(new Vector3(-999, -999, 999)) && !SpawnedFire)
                    {
                        //Projectiles[i] = GameObject.Instantiate(Fire);
                        Projectiles[i].transform.position = Player.transform.position;
                        Projectiles[i].name = FireAIDelay.ToString();
                        //Debug.Log("Spawned fire");
                        SpawnedFire = true;
                    }
                }
                //if (!SpawnedFire)
                    //Debug.Log("Could not spawn fire due to max projectiles");


            }
            else
            {
                //Debug.Log("Fireball did not spawn ["+ outcome+ "<=" + FireAIchance + "]");
            }
        }

        // Activate fireballs if ready

        for (int i = 0; i < Projectiles.Length; i++)
        {
            if (Projectiles[i].name.Equals("0"))
            {
                // check if the player is in zone
                if(Player.transform.position.x.Equals(Projectiles[i].transform.position.x) && Player.transform.position.y.Equals(Projectiles[i].transform.position.y))
                {
                    Debug.Log("Hit player with fireball");
                    Destroy(Player);
                }
                else
                {
                    Debug.Log("Missed player with fireball");
                    //Destroy(Projectiles[i]);
                    Projectiles[i].transform.position = new Vector3(-999, -999, 999);
                    Projectiles[i].name = FireAIDelay.ToString();
                }
            }
            else
            {
                if (!Projectiles[i].name.Equals((FireAIDelay + 1).ToString()))
                {
                    int name = int.Parse(Projectiles[i].name);
                    name -= 1;
                    Projectiles[i].name = name.ToString();
                }
                
            }
        }


        Step++;
        
    }

    public void Update()
    {
        //Debug.Log("Drawing line");
        //Debug
    }
}