using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public Inventory ivtry; // 인벤토리 가져오기
    public ItemPanelOnOff ipoo; // itemPanel 에 붙은 스크립트 가져옴
    Item item;
    private void Start()
    {
        item = null;
    }
    public void findItem()
    {
        GameObject slotParent = transform.parent.gameObject;
        if (slotParent.name == "Slot (1)")
        {
            findItemHelper(0, slotParent);
        }
        else if (slotParent.name == "Slot (2)")
        {
            findItemHelper(1, slotParent);
        }
        else if (slotParent.name == "Slot (3)")
        {
            findItemHelper(2, slotParent);
        }
        else if (slotParent.name == "Slot (4)")
        {
            findItemHelper(3, slotParent);
        }
        else if (slotParent.name == "Slot (5)")
        {
            findItemHelper(4, slotParent);
        }
        else if (slotParent.name == "Slot (6)")
        {
            findItemHelper(5, slotParent);
        }
        else if (slotParent.name == "Slot (7)")
        {
            findItemHelper(6, slotParent);
        }
        else if (slotParent.name == "Slot (8)")
        {
            findItemHelper(7, slotParent);
        }
        else if (slotParent.name == "Slot (9)")
        {
            findItemHelper(8, slotParent);
        }
        else if (slotParent.name == "Slot (10)")
        {
            findItemHelper(9, slotParent);
        }
        else if (slotParent.name == "Slot (11)")
        {
            findItemHelper(10, slotParent);
        }
        else if (slotParent.name == "Slot (12)")
        {
            findItemHelper(11, slotParent);
        }
        else return;
    }
    private void findItemHelper(int i, GameObject slotParent)
    {
        item = ivtry.slots[i].item;
        if (item != null)
        {
            ipoo.setItem(item, slotParent);
            ipoo.ItemPanelOn();
        }
        else ipoo.ItemPanelOff();
    }
}
