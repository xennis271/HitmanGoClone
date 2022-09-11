using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int PlayerStartX = 0;
    public int PlayerStartY = 0;
    public int ZoneX = 0;
    public int ZoneZ = 0;
    public Level(int PlayerStartX, int PlayerStartY, int ZoneX, int ZoneZ)
    {
        this.PlayerStartX = PlayerStartX;
        this.PlayerStartY = PlayerStartY;
        this.ZoneX = ZoneX;
        this.ZoneZ = ZoneZ;
    }
    public int GetZoneX()
    {
        return ZoneX;
    }
    public int GetZoneZ()
    {
        return ZoneZ;
    }
}
public class GameManager : MonoBehaviour
{
    public int GameZoneSizeX = 0;
    public int GameZoneSizeZ = 0;
    public int XBuff = 0;
    public int ZBuff = 0;
    public GameObject Player;
    public List<GameObject> GameZoneCubes;
    public List<Level> GameLevels;
    // Start is called before the first frame update
    void Start()
    {
        //Level Creation
        Level LvOne = new Level(0, 1, 10, 10);
        //GameLevels.Add(LvOne);
        GameZoneSizeX = LvOne.ZoneX;
        GameZoneSizeZ = LvOne.ZoneZ;
        Player.transform.position = new Vector3(LvOne.PlayerStartX, LvOne.PlayerStartY, 0);
        // For the prototype I am hardcoding the size but that can be set with the Level Obj

        bool Black = false;
        for (int i = 0; i < (GameZoneSizeX * GameZoneSizeZ); i++)
        {
            GameObject Block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameZoneCubes.Add(Block);
            Block.transform.position = new Vector3(XBuff, 0, ZBuff);
            Block.name = "GameZone(" + XBuff.ToString() + ",0," + ZBuff.ToString() + ")";
            if (Black)
            {
                Block.GetComponent<Renderer>().material.color = UnityEngine.Color.black;
                Black = false;
            }
            else
            {
                Block.GetComponent<Renderer>().material.color = UnityEngine.Color.white;
                Black = true;
            }
            
            if (XBuff < GameZoneSizeX)
                XBuff++;
            if(ZBuff < GameZoneSizeZ && XBuff == GameZoneSizeX)
            {
                if (Black)
                {
                    Black = false;
                }
                else
                {
                    Black = true;
                }
                XBuff = 0;
                ZBuff++;
            }
                
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
