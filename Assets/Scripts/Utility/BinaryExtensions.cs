using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public static class BinaryExtensions
{
    //Maybe it should save the keys too, so that it doesn't accidentally break something if the keys aren't read in the correct order
    public static void Write<K>(this BinaryWriter writer, Dictionary<K, int> dictionary)
    {
        for (int i = 0; i < dictionary.Count; i++)
        {
            writer.Write(dictionary.ElementAt(i).Value);
            Debug.Log("WRITING Integers: " + dictionary.ElementAt(i).Key + " Value: " + dictionary[dictionary.ElementAt(i).Key]);
        }
    }
    public static void Write<K>(this BinaryWriter writer, Dictionary<K, string> dictionary)
    {
        for (int i = 0; i < dictionary.Count; i++)
        {
            writer.Write(dictionary.ElementAt(i).Value);
            Debug.Log("WRITING Strings: " + dictionary.ElementAt(i).Key + " Value: " + dictionary[dictionary.ElementAt(i).Key]);
        }
    }
    public static void Read<K>(this BinaryReader reader, Dictionary<K, int> dictionary)
    {
        for (int i = 0; i < dictionary.Count; i++)
        {
            dictionary[dictionary.ElementAt(i).Key] = reader.ReadInt32();
            Debug.Log("READING Integers: " + dictionary.ElementAt(i).Key + " Value: " + dictionary[dictionary.ElementAt(i).Key]);
        }
    }
    public static void Read<K>(this BinaryReader reader, Dictionary<K, string> dictionary)
    {
        for (int i = 0; i < dictionary.Count; i++)
        {
            dictionary[dictionary.ElementAt(i).Key] = reader.ReadString();
            Debug.Log("READING Strings: " + dictionary.ElementAt(i).Key + " Value: " + dictionary[dictionary.ElementAt(i).Key]);
        }
    }
}
