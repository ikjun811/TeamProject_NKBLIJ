using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Item item; // ȹ���� ������
   
    //public Image itemImage;  // �������� �̹���
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    // ������ �̹����� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = sprite.color;
        color.a = _alpha;
        sprite.color = color;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        sprite.sprite = item.itemImage;

        SetColor(1);
    }

    // �ش� ������ ������ ���� ������Ʈ
    public void DeleteSlot(int _count)
    {
        ClearSlot();
    }

    // �ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        sprite.sprite = null;
        SetColor(0);
    }
}