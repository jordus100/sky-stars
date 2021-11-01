using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class usefulClassForAll : MonoBehaviour {

    //protected float screenWidthInUnits, screenHeightInUnits, screen_width_in_world_units;
    public static bool gameOn = false;
    public int highscore, lastscore;
    public static bool afterGame = false;
    public DateTime timeAdsRemoved = new DateTime(2000, 1, 1, 12, 0, 0);
    public bool videoAdErrorPanelIsShowed = false;
    /*
    void Awake()
    { 
        screenHeightInUnits = Camera.main.orthographicSize * 2;
   
        screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
        if (((float)Screen.width / Screen.height) > (2.0f / 2.3f))
        {
            float distance_from_middle = Camera.main.orthographicSize * 2 / 2.3f;
            screen_width_in_world_units = distance_from_middle * 2.0f;
        }
        else
        {
            screen_width_in_world_units = screenWidthInUnits;
        }
    }
    */
    public void SendMessageToAll(string message)
    {
        System.Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            go.SendMessage(message, SendMessageOptions.DontRequireReceiver);
        }
    }
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            highscore = data.highscore;
            lastscore = data.lastscore;
            timeAdsRemoved = data.timeAdsRemoved;
        }
        else
        {
            highscore = 0;
            lastscore = 0;
        }
    }
    public void Save(int Highscore, int Lastscore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "playerInfo.dat", FileMode.OpenOrCreate);
        PlayerData data = new PlayerData() { highscore = Highscore, lastscore = Lastscore, timeAdsRemoved = this.timeAdsRemoved };
        bf.Serialize(file, data);
        file.Close();
    }

    void Start()
    {
        Debug.Log(System.DateTime.Now);
    }

}
[Serializable]
class PlayerData
{
    public int highscore;
    public int lastscore;
    public DateTime timeAdsRemoved;
}

