using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;


public class SaveData : Singleton<SaveData>
{
    private void Start() {
        DontDestroyOnLoad(this);
    }
    public void SaveToJeson<T>(List<T> toSave)
    {
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(content);
    }

    private void WriteFile( string contant)
    {
        FileStream fileStream = new FileStream(Application.persistentDataPath+"/save.csv", FileMode.Create);

        using(StreamWriter writer = new StreamWriter (fileStream)){
            writer.Write(contant);
        }
    }
    public static List<T> ReadListFromJSON<T>() {
        string content =  ReadFile(Application.persistentDataPath+"/save.csv");

        if (string.IsNullOrEmpty (content) || content == "{}") {
            return new List<T> ();
        }

        List<T> res = JsonHelper.FromJson<T> (content).ToList ();

        return res;
    }
     private static string ReadFile (string path) {
        if (File.Exists (path)) {
            using (StreamReader reader = new StreamReader (path)) {
                string content = reader.ReadToEnd ();
                return content;
            }
        }
        return "";
    }

}

public static class JsonHelper {
    public static T[] FromJson<T> (string json) {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (json);
        return wrapper.Items;
    }

    public static string ToJson<T> (T[] array) {
        Wrapper<T> wrapper = new Wrapper<T> ();
        wrapper.Items = array;
        return JsonUtility.ToJson (wrapper);
    }

    public static string ToJson<T> (T[] array, bool prettyPrint) {
        Wrapper<T> wrapper = new Wrapper<T> ();
        wrapper.Items = array;
        return JsonUtility.ToJson (wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T> {
        public T[] Items;
    }
}
