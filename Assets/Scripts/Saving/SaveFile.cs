using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class SaveFile 
{
    public Dictionary<string, int> integers = new Dictionary<string, int>();
    public Dictionary<string, string> strings = new Dictionary<string, string>();

    public int Size()
    {
        int integerSize = integers.Count * sizeof(int);
        int stringsSize = 0;
        for (int i = 0; i < strings.Count; i++)
        {
            string value = strings.ElementAt(i).Value;
            stringsSize += value.Length * sizeof(char);
        }

        return integerSize + stringsSize;
    }
    public void ReadFromBinary(ref byte[] bytes, int saveDataVersion)
    {
        Debug.Log("Reading with Binary Reader from a file that is this big: " + bytes.Length);
        using (BinaryReader reader = new BinaryReader(new MemoryStream(bytes)))
        {
            int version = reader.ReadInt32();
            Debug.Assert(version == saveDataVersion, "Version of savedata didn't match"); // Save data version up

            reader.Read(integers);
            reader.Read(strings);
        }
    }
    public void WriteBinary(out byte[] bytes, int saveDataSize)
    {
        using (BinaryWriter writer = new BinaryWriter(new MemoryStream(Size())))
        {
            writer.Write(integers);
            writer.Write(strings);

            writer.BaseStream.Close();
            bytes = (writer.BaseStream as MemoryStream).GetBuffer();
            Debug.Assert(bytes.Length == Size(),
                "Bytes length and expected data size didnt match! Bytes: " + bytes.Length + " Expected: " + Size());
            Debug.Assert(bytes.Length <= saveDataSize,
                "Trying to save more bytes than allowed! Saving: " + bytes.Length + " Allowed: " + saveDataSize);
        }
    }
}
