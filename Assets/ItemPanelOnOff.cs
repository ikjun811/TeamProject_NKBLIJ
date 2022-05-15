using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelOnOff : MonoBehaviour
{
    public Inventory ivtry;
    private GameObject slotParent;
    public GameObject ItemPanel;
    public void ItemPanelOff()
    {
        ItemPanel.SetActive(false);
    }
    public void ItemPanelOn()
    {
        slotParent = transform.parent.gameObject;
        Debug.Log(slotParent.name);
        if (ItemPanel.activeSelf == true)
        {
            ItemPanelOff();
        }
        else
        {
            Vector2 mypos;
            mypos = this.transform.position;
            ItemPanel.transform.position = mypos + new Vector2(50, 150);
            if (slotParent.name == "Slot (1)")
            {
                Item item = ivtry.slots[0].item;
                if (item != null)
                {
                    ItemPanel.SetActive(true);
                    Debug.Log(item.name);
                }
            }
            else if (slotParent.name == "Slot (2)")
            {
                Item item = ivtry.slots[1].item;
                if (item != null)
                {
                    ItemPanel.SetActive(true);
                    Debug.Log(item.name);
                }
            }
        }
    }
}