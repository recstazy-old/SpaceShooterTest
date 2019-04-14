using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

// Universal serializer for any type of object

public class Serializer
{
    readonly string extension = ".sss"; //sss = SpaceShooterSave =)

    string dataPath;

    XmlSerializer ObjSerializer { get; set; }
    
    public void SerializeObject(object obj)
    {
        ObjSerializer = new XmlSerializer(obj.GetType());

#if UNITY_ANDROID

        dataPath = Application.persistentDataPath;

#endif

#if UNITY_EDITOR

        dataPath = Application.dataPath;

#endif

        using (FileStream fs = new FileStream(dataPath + "/" + obj.GetType() + extension, FileMode.Create))
        {
            ObjSerializer.Serialize(fs, obj);
        }
    }

    public object DeserializeObject(object obj)
    {

#if UNITY_ANDROID

        dataPath = Application.persistentDataPath;

#endif

#if UNITY_EDITOR

        dataPath = Application.dataPath;

#endif

        if (File.Exists(dataPath + "/" + obj.GetType() + extension))
        {
            ObjSerializer = new XmlSerializer(obj.GetType());
            
            using (FileStream fs = new FileStream(dataPath + "/" + obj.GetType() + extension, FileMode.Open))
            {
                obj = (System.Object)ObjSerializer.Deserialize(fs);
            }
            Debug.Log("Deserialized");
        }

        return obj;
    }
}
