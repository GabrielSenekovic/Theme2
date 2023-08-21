using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraPan : MonoBehaviour
{
    public GameObject[] corners;
    public float[] dists;
    public int stepsX;
    public int stepsY;
    public float checkWidth;
    public bool[] directionsOpen;
    public TileBase test;
    public GameObject player;
    public float camSpeed = 0.0f;
    public float[] panOffsets = new float[4];

    Vector2[] dirs = new Vector2[4]
        {
            new Vector2(1.0f,0.0f),   //-->
            new Vector2(0.0f, -1.0f), // V
            new Vector2(-1.0f, 0.0f), // <--
            new Vector2(0.0f, 1.0f)  // A
        };

    // Start is called before the first frame update
    void Start()
    {
        panOffsets = new float[] { 2.0f, 2.0f, 0.0f, 0.0f }; // offset x, y, mid point x, y. 

        Vector3 camPos = Camera.main.transform.position;
        //Camera.main.transform.position = new Vector3(camPos.x - panOffsets[0], camPos.y - panOffsets[1], camPos.z);
        dists = new float[2];
        directionsOpen = new bool[4];
        for (int i = 0; i < 2; i++)
        {
            dists[i] = Vector2.Distance(corners[i].transform.position, corners[ (i + 1)].transform.position);
        }

        stepsX =  Mathf.RoundToInt(dists[0] / checkWidth); // make sure this is rounded anyway
        stepsY =  Mathf.RoundToInt(dists[1] / checkWidth);
    }

    private void LateUpdate()                                
    {        
        for(int i = 0; i < 4; i++)
        {
            int dirSteps = dirs[i].x != 0 ? stepsX : stepsY;
            directionsOpen[i] = false;
            for(int s = 0; s < dirSteps; s++)
            {
                Vector2 pos = (Vector2)corners[i].transform.position + dirs[i] * (float)s * checkWidth;
                Vector3Int posI = new Vector3Int((int)pos.x, (int)pos.y, 0);
                // UIManager.Instance.smallTileMap.SetTile(posI, test);
                //UIManager.Instance.smallTileMap.SetColor(posI, Color.red);
                Tilemap tileMap = UIManager.Instance.GetTileMap(TilemapFunction.OBJECT);
                Tilemap modifierMap = UIManager.Instance.GetTileMap(TilemapFunction.TILEMAP);
                if (tileMap.GetTile(posI) == null) // if no big
                {
                   // bool hasSmallTile = true;
                    for(int smy = 0; smy < 2; smy++)
                    {
                        for(int smx = 0; smx < 2; smx++)
                        {
                           if (modifierMap.GetTile(new Vector3Int(posI.x * 2 + smx -1, posI.y * 2 + smy -1, 0 )) == null)
                           {
                                //hasSmallTile = false;
                                directionsOpen[i] = true;
                                //break
                           }
                        }
                    }
                }
            }
        }

        for (int d = 0; d < 4; d++)
        {
           Vector2 start = corners[d].transform.position;
           Vector2 end = corners[((d + 1) % 4)].transform.position;

            if (directionsOpen[d])
            { Debug.DrawLine(start, end, Color.green); }
            else { Debug.DrawLine(start, end, Color.red); }
        }

        Vector3 camPos = Camera.main.transform.position;

        if (directionsOpen[0] && player.transform.position.y > camPos.y + panOffsets[1]) // up
        {
            Camera.main.transform.position = new Vector3(camPos.x, camPos.y + camSpeed, camPos.z);
            //lerp to avoid stutter
        }

        if (directionsOpen[1] && player.transform.position.x > camPos.x + panOffsets[0]) // right
        {
            Camera.main.transform.position = new Vector3(camPos.x + camSpeed, camPos.y, camPos.z);
            //lerp to avoid stutter
        }

        if (directionsOpen[2] && player.transform.position.y < camPos.y - panOffsets[1]) // down
        {
            Camera.main.transform.position = new Vector3(camPos.x, camPos.y - camSpeed, camPos.z);
            //lerp to avoid stutter
        }

        if (directionsOpen[3] && player.transform.position.x < camPos.x - panOffsets[0]) // left
        {
            Camera.main.transform.position = new Vector3(camPos.x - camSpeed, camPos.y, camPos.z);
            //lerp to avoid stutter
        }

    }
}
