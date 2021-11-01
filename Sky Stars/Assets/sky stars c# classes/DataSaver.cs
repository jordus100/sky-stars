using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataSaver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void Save(int Highscore, int Lastscore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "playerInfo.dat", FileMode.OpenOrCreate);
        PlayerData data = new PlayerData() { highscore = Highscore, lastscore = Lastscore };
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            Debug.Log(data.highscore);
            //return data.highscore;
        }
        else
        {
            //return 0;
        }
    }

}
/*
[Serializable]
class PlayerData
{
    public int highscore;
}
*/

    
