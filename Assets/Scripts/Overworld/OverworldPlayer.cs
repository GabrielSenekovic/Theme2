using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OverworldPlayer : MonoBehaviour
{
    public OverworldLevel currentLevel;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector3.down);
        }
        if(Input.GetKeyDown(KeyCode.Space) && currentLevel != null)
        {
            currentLevel.LoadLevel();
        }
    }
    void Move(Vector3 direction)
    {
        Vector3 currentPosition = transform.position + direction;
        Vector3 moveSteps = Vector3.zero;
        while (OverworldManager.Instance.TileExists(currentPosition, ref currentLevel))
        {
            moveSteps += direction;
            currentPosition += direction;
        }
        transform.position += moveSteps;
    }
}
