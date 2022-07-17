using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OverworldPlayer : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && OverworldManager.Instance.TileExists(transform.position + Vector3.up))
        {
            transform.position += Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.A) && OverworldManager.Instance.TileExists(transform.position + Vector3.left))
        {
            transform.position += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && OverworldManager.Instance.TileExists(transform.position + Vector3.right))
        {
            transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.S) && OverworldManager.Instance.TileExists(transform.position + Vector3.down))
        {
            transform.position += Vector3.down;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OverworldLevel level = null;
            if (Physics2D.OverlapCircleAll(transform.position, 0.2f).First(c => c.gameObject.TryGetComponent<OverworldLevel>(out level)) && level != null)
            {
                level.LoadLevel();
            }
        }
    }
}
