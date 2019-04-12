using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

// Universal serializer for any type of object

public class Serializer
{
    readonly string extension = ".xml";

    XmlSerializer ObjSerializer { get; set; }
    
    public void SerializeObject(object obj)
    {
        ObjSerializer = new XmlSerializer(obj.GetType());

        using (FileStream fs = new FileStream(Application.dataPath + "/" + obj.GetType() + extension, FileMode.OpenOrCreate))
        {
            ObjSerializer.Serialize(fs, obj);
        }
    }

    public object DeserializeObject(object obj)
    {
        if (File.Exists(Application.dataPath + "/" + obj.GetType() + extension))
        {
            ObjSerializer = new XmlSerializer(obj.GetType());

            using (FileStream fs = new FileStream(Application.dataPath + "/" + obj.GetType() + extension, FileMode.OpenOrCreate))
            {
                obj = (System.Object)ObjSerializer.Deserialize(fs);
            }
            Debug.Log("Deserialized");
            Debug.Log(obj.GetType());
        }

        return obj;
    }
}
