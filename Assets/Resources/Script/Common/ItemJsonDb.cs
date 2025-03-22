using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEditor;
using System;
using Newtonsoft;
using System.Text;
using Newtonsoft.Json;

public class ItemJsonDb : Singleton<ItemJsonDb>
{

    private JsonData itemData;
    private JsonData inGameItemData;

    private List<Item> listItem = new List<Item>();
    public List<GameItem> ListInGameItem = new List<GameItem>();
    private string filePath = "MyItem.txt";
    public List<List<GameItem>> groupedItemList;

    // Start is called before the first frame update
    void Start()
    {
        LoadResourcesFromTxt();
        ConstructDatabase();
        LoadDataFromLocalDb();
    }

    public void LoadDataFromLocalDb()
    {
        string filePathFull=Application.persistentDataPath+"/"+filePath;
        Debug.Log(filePathFull);
        if(!File.Exists(filePathFull) )
        {
            //chua ton tai
            AddNewItemFirstTime();
            Save();
        }
        else
        {
            byte[] jsonByte=null;
            try
            {
                jsonByte = File.ReadAllBytes(filePathFull);
            }catch(Exception e)
            {
                Debug.Log(e.Message);
            }
            string jsonData=Encoding.ASCII.GetString(jsonByte);
            inGameItemData= JsonMapper.ToObject(jsonData);
            ConstructItemInGame();
        }
    }

    private void Save()
    {
        string jsonData = JsonConvert.SerializeObject(ListInGameItem.ToArray(), Formatting.Indented);
        string filePathFull = Application.persistentDataPath + "/" + filePath;
        byte[] jsonByte = Encoding.ASCII.GetBytes(jsonData);
        if (!Directory.Exists(Path.GetDirectoryName(filePathFull)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePathFull));
        }
        if (!File.Exists(filePathFull))
        {
            File.Create(filePathFull).Close();
        }
        try
        {
            File.WriteAllBytes(filePathFull, jsonByte);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cannot save" + e.Message);
        }
    }

    private void AddNewItemFirstTime()
    {
        for (int i = 0; i < listItem.Count; i++)
        {
            GameItem newGameItem= new GameItem();
            newGameItem.item = listItem[i];
            newGameItem.Purchaerd = false;
            newGameItem.IsUsing = false;
            if (newGameItem.item.Type== "Weapons"&&newGameItem.item.Id==1)
            {
                newGameItem.Purchaerd = true;
                newGameItem.IsUsing = true;
            }
            ListInGameItem.Add(newGameItem);
        }
    }

    private void LoadResourcesFromTxt()
    {
        string filePath = "StreamingAssets/item";
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        itemData = JsonMapper.ToObject(targetFile.text);
    }
    public void ConstructItemInGame()
    {

        for (int i = 0; i < inGameItemData.Count; i++)
        {
            GameItem gameItem = new GameItem();
            gameItem.item = new Item();
            gameItem.item.Id = (int)inGameItemData[i]["item"]["Id"];
            gameItem.item.Type = (string)inGameItemData[i]["item"]["Type"];
            gameItem.item.Price = (int)inGameItemData[i]["item"]["Price"];
            gameItem.item.Atk = (int)inGameItemData[i]["item"]["Atk"];
            gameItem.item.Def = (int)inGameItemData[i]["item"]["Def"];
            gameItem.item.Spd = (int)inGameItemData[i]["item"]["Spd"];
            gameItem.IsUsing = (bool)inGameItemData[i]["IsUsing"];
            gameItem.Purchaerd = (bool)inGameItemData[i]["Purchaerd"];
            ListInGameItem.Add(gameItem);
        }
    }

    public void PurchaseItem(GameItem item)
    {
        for (int i = 0; i < ListInGameItem.Count; i++)
        {
            if (item.item.Type == ListInGameItem[i].item.Type && item.item.Id == ListInGameItem[i].item.Id)
            {
                ListInGameItem[i].Purchaerd = true;
                break;
            }
        }
        Save();
    }
    public void EquipItem(GameItem item)
    {
        UnequipItem(item);
        for (int i = 0; i < ListInGameItem.Count; i++)
        {
            if (item.item.Type == ListInGameItem[i].item.Type && item.item.Id == ListInGameItem[i].item.Id)
            {
                ListInGameItem[i].IsUsing = true;
                break;
            }
        }
        Save();
    }

    public void UnequipItem(GameItem item)
    {
        for (int i = 0; i < ListInGameItem.Count; i++)
        {
            if (item.item.Type == ListInGameItem[i].item.Type)
            {
                ListInGameItem[i].IsUsing = false;
            }
        }
        Save();
    }

    public List<GameItem> GetAllItemInGame(string type)
    {
        List<GameItem > listItems = new List<GameItem>();
        for(int i = 0;i<ListInGameItem.Count;i++)
        {
            if(type == ListInGameItem[i].item.Type)
            {
                listItems.Add(ListInGameItem[i]);
            }
        }
        return listItems;
    }

    public int GetIdItemEquiped(string type)
    {
        for (int i = 0; i < ListInGameItem.Count; i++)
        {
            if (type == ListInGameItem[i].item.Type && ListInGameItem[i].IsUsing == true)
            {
                return ListInGameItem[i].item.Id;
            }
        }
        return 0;
    }
    private void ConstructDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            Item item = new Item();
            item.Id = (int)itemData[i]["Id"];
            item.Type = (string)itemData[i]["Type"];
            item.Price = (int)itemData[i]["Price"];
            item.Atk = (int)itemData[i]["Atk"];
            item.Def = (int)itemData[i]["Def"];
            item.Spd = (int)itemData[i]["Spd"];
            listItem.Add(item);
        }
    }
}

public class GameItem 
{
    public bool Purchaerd { get; set; }
    public bool IsUsing { get; set; }
    public Item item { get; set; }
}

public class Item
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Spd { get; set; }
}