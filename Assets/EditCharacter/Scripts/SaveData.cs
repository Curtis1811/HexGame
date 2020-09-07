using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    public static string path;
    public static void saveData(AbilitySelector ab)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        path = Application.persistentDataPath + "/Data.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(ab);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData loadData()
    {
        string path = Application.persistentDataPath + "/Data.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No File Found " + path);
            return null;
        }
    }
}
