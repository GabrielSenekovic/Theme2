using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Purchasing.MiniJSON;

public class Activate : MonoBehaviour
{
    public GameObject thingToActivate;

    KillPlayer killPlayer;

    private void Start()
    {
        if(killPlayer = GetComponentInChildren<KillPlayer>())
        {
            Vector3Int pos = new Vector3Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(transform.localPosition.y), (int)transform.localPosition.z) * 2;
            GetModifierValue(pos, out ModifierTile.ModifierValue val);
            if (val.HasFlag(ModifierTile.ModifierValue.NoCollision))
            {
                killPlayer.isOn = false;
            }
        }
    }
    bool GetModifierValue(Vector3Int pos, out ModifierTile.ModifierValue val)
    {
        ModifierTile tile = UIManager.Instance.GetTileMap(TilemapFunction.MODIFIER).GetTile(pos + Vector3Int.down) as ModifierTile;
        UIManager.Instance.GetTileMap(TilemapFunction.MODIFIER).SetColor(pos + Vector3Int.down, Color.clear);
        if (tile)
        {
            val = tile.value;
            return true;
        }
        val = 0;
        return false;
    }

    public void Toggle(bool value)
    {
        thingToActivate.SetActive(value);
    }
}
