using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;

public abstract class SessionPersistent
{
    string path = Application.persistentDataPath;

    public virtual void ToFile()
    {
        string completeFilePath = path + "/" + this.GetType().Name + ".json";
        string saveString = JsonUtility.ToJson(this);
        File.WriteAllText(completeFilePath, saveString);
    }
    public virtual T FromFile<T>() where T : new()
    {
        string completeFilePath = path + "/" + typeof(T).Name + ".json";
        if (File.Exists(completeFilePath))
        {
            string json = File.ReadAllText(completeFilePath);
            return JsonUtility.FromJson<T>(json);
        }
        return GetDefault<T>();
    }
    protected virtual T GetDefault<T>() where T : new()
    { 
        ConstructorInfo constructorWithoutParameter= typeof(T).GetConstructor(System.Type.EmptyTypes);
        return (T) constructorWithoutParameter.Invoke(new object[0]); 
    }
}
