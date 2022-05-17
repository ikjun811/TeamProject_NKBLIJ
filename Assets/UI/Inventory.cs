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
            items.Add(_item);  // items List에 _item 추가
            FreshSlot(); // 슬롯에 아이템 이미지 표시
        } 
        else 
        { 
            print("인벤토리 초과"); // 발생 가능성 있을까?
        }
    }
    public void RemoveItem(string itemName)
    {
        int k = 0;
        for (int i = 0; i < items.Count && i < slots.Length; i++)
        {
            k++;
            if (slots[i].item.name == itemName)
            {
                k++;
                items.Remove(slots[i].item);
                FreshSlot();
                break;
            }
            if (k > 100) break;
        }
    }
}
