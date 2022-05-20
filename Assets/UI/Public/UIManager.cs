using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool IsUIOn;

    public GameObject SettingPanel;
    public GameObject InventoryPanel;
    public GameObject NewItemAddPanel;
    public Text NewItemAddText;
    public GameObject ItemNameText;
    public GameObject ItemInfoText;
    public ItemPanel ip; // itemPanel 에 붙은 스크립트 가져옴

    private void Start()
    {
        IsUIOn = false;
    }

    public void SetPanelOff()
    {
        SettingPanel.SetActive(false);
        IsUIOn = false;
    }
    public void SetPanelOn()
    {
        if (SettingPanel.activeSelf == true)
        {
            SettingPanel.SetActive(false);
            IsUIOn = false;
        }
        else if (IsUIOn)
        {
            return;
        }
        else
        {
            SettingPanel.SetActive(true);
            IsUIOn = true;
        }
    }
    public void  InventoryOn()
    {
        if (InventoryPanel.activeSelf == true)
        {
            InventoryPanel.SetActive(false);
            IsUIOn = false;
        }
        else if (IsUIOn)
        {
            return;
        }
        else
        {
            InventoryPanel.SetActive(true);
            IsUIOn = true; 
        }
    }
    public void InventoryOff()
    {
        ip.ItemPanelOff();
        ItemNameInfoTextOff();
        InventoryPanel.SetActive(false);
        IsUIOn = false;
    }
    public void NewItemAddPanelOn(string str)
    {
        IsUIOn = true;
        NewItemAddText.GetComponent<Text>().text = str;
        NewItemAddPanel.SetActive(true);
    }
    public void NewItemAddPanelOff()
    {
        IsUIOn = false;
        NewItemAddPanel.SetActive(false);
    }
    public void ItemNameInfoTextOn(string name, string info)
    {
        ItemNameText.GetComponent<Text>().text = name;
        ItemInfoText.GetComponent<Text>().text = info;
        ItemNameText.SetActive(true);
        ItemInfoText.SetActive(true);
    }
    public void ItemNameInfoTextOff()
    {
        ItemNameText.SetActive(false);
        ItemInfoText.SetActive(false);
    }
}
