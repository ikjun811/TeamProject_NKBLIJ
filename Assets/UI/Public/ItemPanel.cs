using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public UIManager um;
    public Inventory ivtry;
    public GameObject itemPanel;
    public GameObject NowState; // ����� Text

    public Item selecteditem; // ������ ������
    private GameObject slotParent;
    public bool combineFlag;

    private void Start()
    {
        combineFlag = false;
    }

    public void setItem(Item item, GameObject Parent) // ���� ������ ������ �޾ƿ�
    {
        selecteditem = item;
        slotParent = Parent;
    }
    public string getItem()
    {
        if (selecteditem == null) return " ";
        else return selecteditem.name;
    }
    public void ItemPanelOff()
    {
        um.ItemNameInfoTextOff();
        itemPanel.SetActive(false);
    }
    public void ItemPanelOn() // ������ ������ �г� on
    {
        if (itemPanel.activeSelf == true) // �̹� ���� ���¿��� �ٽ� �������� 
        {
            ItemPanelOff(); // ����
        }
        else
        {
            Vector2 mypos;
            mypos = slotParent.transform.position;
            if (mypos.y < 300) // �������� �ߺ����� �������� ���� ���� �ڵ� -> �� �̵����� �߻�
            {
                itemPanel.transform.position = mypos + new Vector2(50, 150);
            }
            itemPanel.SetActive(true);
            um.ItemNameInfoTextOn(selecteditem.itemName, selecteditem.itemInfo);
        }
    }
    public void usingItem() // ������ ��� ��ư �̺�Ʈ
    {
        ItemPanelOff(); // ������ ������ �г� �ݰ�
        um.InventoryOff(); // �κ��丮 �ݰ�
        um.IsUIOn = false; // ��ġ �����ϰ� �����ְ�
        NowState.GetComponent<Text>().text = "������ ��� : " + selecteditem.itemName;
        NowState.SetActive(true); // ���� �ؽ�Ʈ ǥ��
    }

    public void destroyItem() // ������ ���� ��ư �̺�Ʈ
    {
        ItemPanelOff(); // ������ ������ �г� �ݰ�
        if ( selecteditem.name == "Box") // Box �����ϸ� Gas ���
        {
            NowState.SetActive(false); // ���� �ۻ����->�κ��丮->�����Ҽ��� ������ ����
            Item item = Resources.Load("Item/5F/StartRoom/Gas") as Item; // �� ������ ������ ��������
            ivtry.RemoveItem(selecteditem.name);
            ivtry.AddItem(item);
            um.NewItemAddPanelOn("������ ȹ�� : ����");
        }
        else
        {
            um.ItemNameInfoTextOn("������ ���� ����","���� �Ұ� ������"); 
            return;
        }
    }
    public void combineItem() // ������ ���� ��ư �̺�Ʈ
    {
        combineFlag = true;
        NowState.SetActive(false);
        ItemPanelOff();
        um.ItemNameInfoTextOn("������ ����", selecteditem.itemName + "\n+\n���� ��� ������ ����");
    }

}