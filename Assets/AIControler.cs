using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControler : MonoBehaviour
{
    public GameObject[] BasicAI;
    public Dictionary<GameObject,List<Vector3>> BasicAIMoves = new Dictionary<GameObject,List<Vector3>>();
    public void Start()
    {
        foreach(GameObject AI in BasicAI)
        {
            List<GameObject> AllChildren = new List<GameObject>();
            

            // first off let's get the Gos of all kids
            foreach (Transform child in AI.transform)
            {
                AllChildren.Add(child.gameObject);
            }
            // we have all the kids kick them to get the correct pos in realtion to the world
            AI.transform.DetachChildren();
            List<Vector3> MovePoints = new List<Vector3>();
            // add self so we have the starting point
            MovePoints.Add(AI.transform.position);
            foreach (GameObject node in AllChildren)
            {
                //Debug.Log("Node:" + int.Parse(node.name));
                MovePoints.Insert(int.Parse(node.name)+1, node.transform.position);
                // remove the node after
                GameObject.Destroy(node);
            }
            // put this in the mega list
            BasicAIMoves.Add(AI, MovePoints);
            //MovePoints.Clear();
            //AllChildren.Clear();
        }
    }



    public int Step = 1;
    public void MoveAI()
    {
        
        foreach (GameObject AI in BasicAI)
        {
            // move to the correct spot.
            //Debug.Log(Step+1 + ">" + BasicAIMoves[AI].Count);
            if(Step >= BasicAIMoves[AI].Count)
            {
                // fewer steps than the count so just keep a fake step
                int FakeStep = Step;
                while(FakeStep >= BasicAIMoves[AI].Count)
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
                Debug.Log("Step:" + Step);
                AI.transform.position = BasicAIMoves[AI][Step];
            }
            
        }
        Step++;
    }
}
