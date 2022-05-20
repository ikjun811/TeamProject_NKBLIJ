using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot_item: MonoBehaviour  // 슬롯에 아이템 이미지 넣어주는 스크립트
{
    [SerializeField] 
    Image image; 
    
    private Item _item; 
    public Item item 
    { 
        get 
        { 
            return _item; 
        } 
        set 
        { 
            _item = value; 
            if (_item != null) // 설정값을 받으면
            { 
                image.sprite = item.itemImage; 
                image.color = new Color(1, 1, 1, 1); // RGBA 1로 설정하고 itemImage Sprite 삽입
            }
            else 
            { 
                image.color = new Color(1, 1, 1, 0); // 투명화
            } 
        } 
    } 
}
