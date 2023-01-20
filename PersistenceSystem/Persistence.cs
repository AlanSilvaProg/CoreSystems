using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Persistence : IPersistence
{
    private BinaryFormatter formatter = new BinaryFormatter();

    private string GetPath(string saveFileName) => $"{Application.persistentDataPath}{saveFileName}.unitydata";

    private bool SaveAlreadyExist(string saveFileName) => File.Exists(GetPath(saveFileName));

    public T Load<T>(string saveFileName)
    {
        if (SaveAlreadyExist(saveFileName))
        {
            FileStream stream = new FileStream(GetPath(saveFileName), FileMode.Open);
            T loadedInfo = (T)formatter.Deserialize(stream);
            stream.Close();
            return loadedInfo;
        }
        Debug.LogError($"Without Load Arquive: {saveFileName}");
        return default(T); 
    }

    public void Save<T>(T information, string saveFileName)
    {
        if (SaveAlreadyExist(saveFileName))
            DeleteSave(saveFileName);

        FileStream stream = new FileStream(GetPath(saveFileName), FileMode.Create);
        formatter.Serialize(stream, information);

        stream.Close();
        Debug.Log($"Save completed: {GetPath(saveFileName)}");
    }

    public void DeleteSave(string saveFileName)
    {
        File.Delete(GetPath(saveFileName));
    }
}
