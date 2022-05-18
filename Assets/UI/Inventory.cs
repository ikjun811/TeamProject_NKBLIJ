using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour 
{ 
    public List<Item> items;
    [SerializeField] 
    private Transform slotParent;
    [SerializeField] 
    public Slot_item[] slots; 
    private void OnValidate() 
    { 
        slots = slotParent.GetComponentsInChildren<Slot_item>(); 
    } 
    void Awake() 
    { 
        FreshSlot(); 
    } 
    public void FreshSlot() 
    { 
        int i = 0; 
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i]; 
        } 
        for (; i < slots.Length; i++)
        { 
            slots[i].item = null; 
        } 
    } 
    public void AddItem(Item _item) 
    { 
        if (items.Count < slots.Length) 
        {
            items.Add(_item);  // items List�� _item �߰�
            FreshSlot(); // ���Կ� ������ �̹��� ǥ��
        } 
        else 
        { 
            print("�κ��丮 �ʰ�"); // �߻� ���ɼ� ������?
        }
    }
    public void RemoveItem(string itemName)
    {
        for (int i = 0; i < items.Count && i < slots.Length; i++)
        {
            if (slots[i].item.name == itemName)
            {
                items.Remove(slots[i].item);
                FreshSlot();
                break;
            }
        }
    }
    public bool FindItem(string itemName)
    {
        for (int i = 0; i < items.Count && i < slots.Length; i++)
        {
            if (slots[i].item.name == itemName)
            {
                return true;
            }
        }
        return false;
    }
}
