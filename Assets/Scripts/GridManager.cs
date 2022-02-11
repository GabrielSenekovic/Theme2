using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap content;
    public Tilemap tilemap;

    private void Start()
    {
        UIManager.Instance.tilemap = tilemap;
        UIManager.Instance.contentmap = content;
    }
}
