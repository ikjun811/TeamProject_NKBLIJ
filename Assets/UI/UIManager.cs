using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool IsUIOn;

    public GameObject SettingPanel;
    public GameObject InventoryPanel;

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
        InventoryPanel.SetActive(false);
        IsUIOn = false;
    }
}
