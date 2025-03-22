using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    public GameObject itemPrefabs;
    public GameObject parent;
    public Button[] listButton;
    private static string[] listType = { "Weapons", "Hat", "Pants", "Shield" };
    public Button btnBack;
    // Start is called before the first frame update
    void Start()
    {
        ClickButtonType(0);
        listButton[0].onClick.AddListener(() => ClickButtonType(0));
        listButton[1].onClick.AddListener(() => ClickButtonType(1));
        listButton[2].onClick.AddListener(() => ClickButtonType(2));
        listButton[3].onClick.AddListener(() => ClickButtonType(3));
        btnBack.onClick.AddListener(() => ClickBack());

    }

    private void ClickBack()
    {
        UIManager.Instance.shopPanel.SetActive(false);
        UIManager.Instance.mainMenu.SetActive(true);
        UIManager.Instance.InitGameState(1);
    }

    private void ClickButtonType(int type)
    {
        for (int i = 0; i < 4; i++)
        {
            listButton[i].GetComponent<Image>().color = Color.white;
        }
        listButton[type].GetComponent<Image>().color = Color.yellow;
        CreateItem(listType[type]);
    }

    public void CreateItem(string type)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        List<GameItem> itemIngame = ItemJsonDb.Instance.GetAllItemInGame(type);
        
        for (int i = 0; i < itemIngame.Count; i++)
        {
            GameObject item = Instantiate(itemPrefabs, parent.transform);
            item.GetComponent<InitItem>().InitItemUI(itemIngame[i]);
        }
    }
}
