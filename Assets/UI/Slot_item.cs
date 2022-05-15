using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot_item: MonoBehaviour  // ���Կ� ������ �̹��� �־��ִ� ��ũ��Ʈ
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
            if (_item != null) // �������� ������
            { 
                image.sprite = item.itemImage; 
                image.color = new Color(1, 1, 1, 1); // RGBA 1�� �����ϰ� itemImage Sprite ����
            }
            else 
            { 
                image.color = new Color(1, 1, 1, 0); // ����ȭ
            } 
        } 
    } 
}