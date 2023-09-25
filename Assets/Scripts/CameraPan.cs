using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

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

    Tilemap objectsMap;
    Tilemap tileMap;
    Tilemap CameraMap;

    public enum WALLS { LEFT, RIGHT, BOTH, NONE };

    public WALLS closestWall;

    Vector2[] dirs = new Vector2[4]
        {
            new Vector2(1.0f,0.0f),   //right
            new Vector2(0.0f, -1.0f), //down
            new Vector2(-1.0f, 0.0f), //left
            new Vector2(0.0f, 1.0f)   //up
        };

    // Start is called before the first frame update
    void Start()
    {
        objectsMap = UIManager.Instance.GetTileMap(TilemapFunction.OBJECT);
        tileMap = UIManager.Instance.GetTileMap(TilemapFunction.TILEMAP);
        CameraMap = UIManager.Instance.GetTileMap(TilemapFunction.CAMERA);

        panOffsets = new float[] { 1.0f, 1.0f, 0.0f, 0.0f }; // offset x, y, mid point x, y. 

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

        CalibratePosition();
    }

    void CalibratePosition()
    {
        transform.position = player.transform.position - new Vector3(0, Camera.main.orthographicSize * 2 - 0.4f);

        // cast ray left and right
        // check blocks vertical from ray point
        // if one only solid then move away cam width from that
        // if both solid move away from closest
        // if no solid set x  to player x

        Vector2 rightRayHit = CheckForWalls(true);
        Vector2 leftRayHit = CheckForWalls(false);

        float distLeftRay = 0;
        float distRightRay = 0;
        if(CheckVerticalWall(rightRayHit) && rightRayHit != (Vector2)player.transform.position)
        {
            distRightRay = ((Vector2)player.transform.position - rightRayHit).magnitude;
        }
        else if (CheckVerticalWall(leftRayHit) && leftRayHit != (Vector2)player.transform.position)
        {
            distLeftRay = ((Vector2)player.transform.position - leftRayHit).magnitude;
        }

        if (distRightRay > distLeftRay && (distLeftRay * distRightRay != 0))
        {
            transform.position = player.transform.position + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0);
        }
        else if (distRightRay < distLeftRay && (distLeftRay * distRightRay != 0))
        {
            transform.position = player.transform.position - new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0);
        }
        else if (distLeftRay * distRightRay != 0)
        {
            transform.position = player.transform.position + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0);
        }
        else
        {
            Vector3 camPos = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(player.transform.position.x, camPos.y, camPos.z);
        }

    }

    private Vector2 CheckForWalls(bool goingRight)
    {
        Vector3 raycastDirection = (goingRight) ? Vector3.right : Vector3.left;
        RaycastHit2D hit = Physics2D.RaycastAll(player.transform.position - new Vector3(0, 0.0f, 0), raycastDirection, 20) // 20 = camera width in tiles
          .FirstOrDefault(h => h.transform.CompareTag("Tilemap"));

        //Debug.Log("hit: " + (hit.point == null) );

        if (hit.point != null)
        {
            //Debug.Log("collider: " + (hit.collider));
            if (hit.transform.tag == "Tilemap")
            {
                Debug.Log("tilemap hit: " + hit.point);
                Vector2 output = (goingRight) ? hit.point : new Vector2(hit.point.x - 1.0f, hit.point.y);
                return output;
            }
        }

        return player.transform.position;
    }

    private bool CheckVerticalWall(Vector2 pos)
    {
        for (int y = 0; y < 12; y++)
        {
            Vector3Int checkingPosition = new Vector3Int((int)(pos.x), (int)(pos.y) -1 + y, 0);
            objectsMap.SetColor(checkingPosition, Color.red);
            //GameObject g = new GameObject();
            //g.transform.position = checkingPosition;
            //GameObject.Instantiate(g);

            if (CameraMap.GetTile(checkingPosition) == null)
            {
                return false;
            }
        }
        return true;
    }


    void CheckForSolidTileWallsOnCameraEdge()
    {
        for (int i = 0; i< 4; i++)
        {
            int stepsInDirection = dirs[i].x != 0 ? stepsX : stepsY;
            directionsOpen[i] = false;
            for(int steps = 0; steps<stepsInDirection; steps++)
            {
                Vector2 pos = (Vector2)corners[i].transform.position + dirs[i] * (float)steps * checkWidth;
                Vector3Int posI = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0);
                if (CameraMap.GetTile(posI) == null) // if no big
                {
                    directionsOpen[i] = true;
                }
            }
        }
    }

  /*  void CheckForSolidTileWallsOnCameraEdge()
    {
        Tilemap objects = UIManager.Instance.GetTileMap(TilemapFunction.OBJECT);
        Tilemap tileMap = UIManager.Instance.GetTileMap(TilemapFunction.TILEMAP);

        for (int i = 0; i < 4; i++)
        {
            int stepsInDirection = dirs[i].x != 0 ? stepsX : stepsY;
            directionsOpen[i] = false;
            for (int steps = 0; steps < stepsInDirection; steps++)
            {
                Vector2 pos = (Vector2)corners[i].transform.position + dirs[i] * (float)steps * checkWidth;
                Vector3Int posI = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0);
                // UIManager.Instance.smallTileMap.SetTile(posI, test);
                if (objects.GetTile(posI) == null) // if no big
                {
                    // bool hasSmallTile = true;
                    for (int smy = -1; smy < 1; smy++)
                    {
                        for (int smx = -1; smx < 1; smx++)
                        {
                            Vector3Int checkingPosition = new Vector3Int(posI.x * 2 + smx, posI.y * 2 + smy, 0);
                            //tileMap.SetColor(checkingPosition, Color.red);
                            if (tileMap.GetTile(checkingPosition) == null)
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
    }*/

    private void LateUpdate()                                
    {
        CheckForSolidTileWallsOnCameraEdge();

        DrawDebugLines();
        MoveCamera();
    }
    void DrawDebugLines()
    {
        for (int d = 0; d < 4; d++)
        {
            Vector2 start = corners[d].transform.position;
            Vector2 end = corners[((d + 1) % 4)].transform.position;

            if (directionsOpen[d])
            { Debug.DrawLine(start, end, Color.green); }
            else { Debug.DrawLine(start, end, Color.red); }
        }
    }
    void MoveCamera()
    {
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
