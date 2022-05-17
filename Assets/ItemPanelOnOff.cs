using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelOnOff : MonoBehaviour
{
    public UIManager um;
    public Inventory ivtry;
    public GameObject itemPanel;
    public GameObject NowState; // ����� Text

    private bool usingFlag;
    private Item selecteditem; // ������ ������
    private GameObject slotParent;

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
            if (mypos.y < 300) // new Vector2�� �ߺ����� �������� ���� ���� �ڵ� -> �� �̵����� �߻�
            {
                itemPanel.transform.position = mypos + new Vector2(50, 150);
            }
            itemPanel.SetActive(true);
        }
    }
    public void usingItem() // ������ ��� ��ư �̺�Ʈ
    {
        ItemPanelOff(); // ������ ������ �г� �ݰ�
        um.InventoryOff(); // �κ��丮 �ݰ�
        um.IsUIOn = false; // ��ġ �����ϰ� �����ְ�
        NowState.SetActive(true); // ���� �ؽ�Ʈ ǥ��
        NowState.GetComponent<Text>().text = "������ ��� : " + selecteditem.name; // �޼��� ���� ����
    }


    public void destroyItem() // ������ ����
    {

    }


}