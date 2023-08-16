using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ModifierTile : RuleTile
{
    [Flags]
    public enum ModifierValue
    {
        None = 0,
        NoCollision = 1,
        Up = 1 << 1,
        Down = 1 << 2,
        Left = 1 << 3,
        Right = 1 << 4
    }
    public ModifierValue value;
}
