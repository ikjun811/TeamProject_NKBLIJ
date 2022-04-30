using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Item item; // 획득한 아이템
   
    //public Image itemImage;  // 아이템의 이미지
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = sprite.color;
        color.a = _alpha;
        sprite.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        sprite.sprite = item.itemImage;

        SetColor(1);
    }

    // 해당 슬롯의 아이템 갯수 업데이트
    public void DeleteSlot(int _count)
    {
        ClearSlot();
    }

    // 해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        item = null;
        sprite.sprite = null;
        SetColor(0);
    }
}