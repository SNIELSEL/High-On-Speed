using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.LowLevel;

public class SaveAndLoad : MonoBehaviour
{
    [Header("SettingsData")]
    public int slot;

    public void Awake()
    {
        slot = 1;
    }
    public void Start()
    {
        slot = PlayerPrefs.GetInt("SaveSlot");

        if (slot == 0)
        {
            slot++;
        }
    }

    public void SetSlot(int selectedslot)
    {
        slot = selectedslot;

        PlayerPrefs.SetInt("SaveSlot", slot);
    }

    public void SaveData()
    {
        XML_SaveData fileData = new XML_SaveData();

        //hieronder alles zetten wat opgeslaan moet worden
        fileData.slot = slot;

        XmlSerializer serializer = new XmlSerializer(typeof(XML_SaveData));

        using (FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" +slot + ".xml", FileMode.Create))
        {
            serializer.Serialize(stream, fileData);
        }
    }
    public void LoadData()
    {
        XML_SaveData fileData = new XML_SaveData();
        
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XML_SaveData));

            using (FileStream stream = new FileStream(Application.persistentDataPath + "/GameData" + slot + ".xml", FileMode.Open))
            {
                fileData = serializer.Deserialize(stream) as XML_SaveData;

                //hieronder alles zetten wat geladen moet worden
                slot = fileData.slot;
            }
        }
        catch
        {
            SaveData();
        }
    }

}
    [System.Serializable]
    public class XML_SaveData
    {
        public int slot;
    }