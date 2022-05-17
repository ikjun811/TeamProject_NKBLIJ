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
            item = ivtry.slots[0].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (2)")
        {
            item = ivtry.slots[1].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (3)")
        {
            item = ivtry.slots[2].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (4)")
        {
            item = ivtry.slots[3].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (5)")
        {
            item = ivtry.slots[4].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (6)")
        {
            item = ivtry.slots[5].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (7)")
        {
            item = ivtry.slots[6].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (8)")
        {
            item = ivtry.slots[7].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (9)")
        {
            item = ivtry.slots[8].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (10)")
        {
            item = ivtry.slots[9].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (11)")
        {
            item = ivtry.slots[10].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else if (slotParent.name == "Slot (12)")
        {
            item = ivtry.slots[11].item;
            if (item != null)
            {
                ipoo.setItem(item, slotParent);
                ipoo.ItemPanelOn();
            }
        }
        else return;
    }
    
}
