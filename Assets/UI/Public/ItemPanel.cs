using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    public UIManager um;
    public Inventory ivtry;
    public GameObject itemPanel;
    public GameObject NowState; // 사용중 Text

    public Item selecteditem; // 선택한 아이템
    private GameObject slotParent;
    public bool combineFlag;

    private void Start()
    {
        combineFlag = false;
    }

    public void setItem(Item item, GameObject Parent) // 누른 아이템 정보를 받아옴
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
    public void ItemPanelOn() // 아이템 선택지 패널 on
    {
        if (itemPanel.activeSelf == true) // 이미 켜진 상태에서 다시 눌렀으면 
        {
            ItemPanelOff(); // 끄기
        }
        else
        {
            Vector2 mypos;
            mypos = slotParent.transform.position;
            if (mypos.y < 300) // 보정값이 중복으로 더해지는 것을 막는 코드 -> 씬 이동에서 발생
            {
                itemPanel.transform.position = mypos + new Vector2(50, 150);
            }
            itemPanel.SetActive(true);
            um.ItemNameInfoTextOn(selecteditem.itemName, selecteditem.itemInfo);
        }
    }
    public void usingItem() // 아이템 사용 버튼 이벤트
    {
        ItemPanelOff(); // 아이템 선택지 패널 닫고
        um.InventoryOff(); // 인벤토리 닫고
        um.IsUIOn = false; // 터치 가능하게 열어주고
        NowState.GetComponent<Text>().text = "아이템 사용 : " + selecteditem.itemName;
        NowState.SetActive(true); // 상태 텍스트 표시
    }

    public void destroyItem() // 아이템 분해 버튼 이벤트
    {
        ItemPanelOff(); // 아이템 선택지 패널 닫고
        if ( selecteditem.name == "Box") // Box 분해하면 Gas 얻기
        {
            NowState.SetActive(false); // 만약 템사용중->인벤토리->분해할수도 있으니 꺼줌
            Item item = Resources.Load("Item/5F/StartRoom/Gas") as Item; // 새 아이템 프리팹 가져오기
            ivtry.RemoveItem(selecteditem.name);
            ivtry.AddItem(item);
            um.NewItemAddPanelOn("아이템 획득 : 가스");
        }
        else
        {
            um.ItemNameInfoTextOn("아이템 분해 실패","분해 불가 아이템"); 
            return;
        }
    }
    public void combineItem() // 아이템 조합 버튼 이벤트
    {
        combineFlag = true;
        NowState.SetActive(false);
        ItemPanelOff();
        um.ItemNameInfoTextOn("아이템 조합", selecteditem.itemName + "\n+\n조합 대상 아이템 선택");
    }

}