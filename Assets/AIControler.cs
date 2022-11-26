using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControler : MonoBehaviour
{
    public GameObject[] BasicAI;
    public Dictionary<GameObject,List<Vector3>> BasicAIMoves = new Dictionary<GameObject,List<Vector3>>();
    public Dictionary<GameObject, List<string>> BasicAIView = new Dictionary<GameObject, List<string>>();
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
            List<string> ViewPoints = new List<string>();
            
            foreach (GameObject node in AllChildren)
            {
                
                if(!node.name.Equals("F") && !node.name.Equals("B") && ViewPoints.Count < AllChildren.Count-2)
                    ViewPoints.Add("");
                
            }
            //Debug.Log("ViewPoint.Count :" + ViewPoints.Count);
            // add self so we have the starting point
            MovePoints.Add(AI.transform.position);
            foreach (GameObject node in AllChildren)
            {
                char[] line = node.name.ToCharArray();
                if(line.Length != 1)
                {
                   int IndexChar = int.Parse(line[1].ToString());
                   char ViewChar = line[0];
                   //Debug.Log("Name:" + node.name + " I:" + IndexChar + " V:" + ViewChar);
                   MovePoints.Insert(IndexChar + 1, node.transform.position);
                   ViewPoints[IndexChar] = ViewChar.ToString();
                   // remove the node after
                   GameObject.Destroy(node);
                    
                }
                else
                {
                    node.gameObject.transform.parent = AI.transform;
                }
                
            }
            //Debug.Log("ViewPoint.Count 2:" + ViewPoints.Count);
            //int count = 0;
            //foreach (string Line in ViewPoints)
            //{

            //    Debug.Log(count +  ":" + Line);
                
            //    count++;
            //}
            // put this in the mega list
            BasicAIMoves.Add(AI, MovePoints);
            BasicAIView.Add(AI, ViewPoints);
            //MovePoints.Clear();
            //AllChildren.Clear();
        }
    }



    public int Step = 1;
    public void MoveAI()
    {
        
        foreach (GameObject AI in BasicAI)
        {
            //view check
            int count = 0;
            foreach (string line in BasicAIView[AI])
            {
                Debug.Log(count + ":" + line);
                if (line == "")
                    break;
                count++;
            }
            BasicAIView[AI].Capacity = count;
            count = 0;
            foreach (string line in BasicAIView[AI])
            {
                Debug.Log(count + ":" + line);
               // if (line == "")
               //     break;
                count++;
            }
            //MOVEMENT
            // move to the correct spot.
            //Debug.Log(Step+1 + ">" + BasicAIMoves[AI].Count);
            if (Step >= BasicAIMoves[AI].Count)
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
                //VIEW
                if (BasicAIView[AI][FakeStep-1] == "F")
                {
                    foreach (Transform child in AI.transform)
                    {
                        if (child.name.Equals("F"))
                        {
                            Debug.Log("SET F true");
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
                if (BasicAIView[AI][FakeStep-1] == "B")
                {
                    foreach (Transform child in AI.transform)
                    {
                        if (child.name.Equals("B"))
                        {
                            Debug.Log("SET B true");
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Step:" + Step);
                //VIEW
                if (BasicAIView[AI][Step-1] == "F")
                {
                    foreach (Transform child in AI.transform)
                    {
                        if (child.name.Equals("F"))
                        {
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
                if (BasicAIView[AI][Step-1] == "B")
                {
                    foreach (Transform child in AI.transform)
                    {
                        if (child.name.Equals("B"))
                        {
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
                AI.transform.position = BasicAIMoves[AI][Step];
            }
            
        }
        
        
        Step++;
    }
}
