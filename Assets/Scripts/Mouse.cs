using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[ExecuteInEditMode]
public class Mouse : MonoBehaviour
{
    public Tilemap map;

    bool added = false;

    float timer = 0;

    private void Update() 
    {
        if(!added)
        {
            SceneView.duringSceneGui += SaveMouse;
            added = false;
        }
    }

    private void SaveMouse(SceneView sceneView)
    {
        timer++;
        if(timer < 10){return;}else{timer = 0;}
        Vector3 mousePosition = Event.current.mousePosition;
        mousePosition = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition).origin;
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);

        //Debug.Log("MousePos: " + mousePosition);
        //TilemapHelper.mousePosition = map.WorldToCell(mousePosition);
       // Debug.Log("Mouseposition is: " + TilemapHelper.mousePosition);
    }
}
