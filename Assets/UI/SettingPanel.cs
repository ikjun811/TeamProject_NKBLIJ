using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{

    public GameObject thisPanel;
    public void SetPanelOff()
    {
        thisPanel.SetActive(false);
    }
    public void SetPanelOn()
    {
        thisPanel.SetActive(true);
    }
}
