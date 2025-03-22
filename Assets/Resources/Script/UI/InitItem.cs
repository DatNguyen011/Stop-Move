using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitItem : MonoBehaviour
{
    public Image itemImage;
    public GameObject price;
    public TextMeshProUGUI priceText;
    public Button actionButton;
    public TextMeshProUGUI textButton;
    public GameObject panel;
    int state = 0;
    public GameItem thisItem;

    void Start()
    {
        actionButton.onClick.AddListener(() => ButtonAcition());
    }

    public void ButtonAcition()
    {
        if (state == 1)
        {
            int currentGold = GameController.Instance.gold;
            int priceItem = thisItem.item.Price;
            if (currentGold >= priceItem)
            {
                GameController.Instance.ReduceGold(priceItem);
                ItemJsonDb.Instance.PurchaseItem(thisItem);
                ShopManager.Instance.CreateItem(thisItem.item.Type);
            }
        }
        else if (state == 2) 
        {
            ItemJsonDb.Instance.EquipItem(thisItem);
            ShopManager.Instance.CreateItem(thisItem.item.Type);
            GameController.Instance.InitPlayerItems();
        }
        else if (state == 3) 
        {
            ItemJsonDb.Instance.UnequipItem(thisItem);
            ShopManager.Instance.CreateItem(thisItem.item.Type);
            GameController.Instance.InitPlayerItems();
        }
    }

    public void InitItemUI(GameItem item)
    {
        thisItem = item;
        itemImage.sprite = Resources.Load<Sprite>("UI/" + item.item.Type + "/" + item.item.Id);
       
        if (item.Purchaerd == false)
        {
            state = 1;
            price.SetActive(true);
            priceText.text = item.item.Price.ToString();
        }
        else
        {
            price.SetActive(false);
            if (item.IsUsing)
            {
                state = 3;
            }
            else
            {
                state = 2;
            }
        }
        InitButtonState();
    }

    private void InitButtonState()
    {
        if (state == 1)
        {
            textButton.text = "Buy";
            actionButton.GetComponent<Image>().color = Color.white;
            
        }
        else if (state == 2)
        {
            actionButton.GetComponent<Image>().color = Color.green;
            textButton.text = "Equip";
            price.SetActive(false);
            panel.SetActive(false);
            
        }
        else if (state == 3)
        {
            textButton.text = "Used";
            actionButton.GetComponent<Image>().color = Color.yellow;
            price.SetActive(false);
            panel.SetActive(false);
        }
       
    }
}
