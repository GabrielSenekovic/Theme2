using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 public static class Commands
 {
     [MenuItem("My Commands/Decrease &q")]
     static void Decrease() 
     {
        TilemapHelper.index = TilemapHelper.index > 0 ? TilemapHelper.index - 1: TilemapHelper.index;
        Debug.Log("Index is now: " + TilemapHelper.index );
     }
     [MenuItem("My Commands/Increase &t")]
     static void Increase() 
     {
        TilemapHelper.index++;
        Debug.Log("Index is now: " + TilemapHelper.index );
     }
 }
