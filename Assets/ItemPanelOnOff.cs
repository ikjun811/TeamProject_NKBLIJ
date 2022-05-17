using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelOnOff : MonoBehaviour
{
    public UIManager um;
    public Inventory ivtry;
    public GameObject itemPanel;
    public GameObject NowState; // 사용중 Text

    private bool usingFlag;
    private Item selecteditem; // 선택한 아이템
    private GameObject slotParent;

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
            if (mypos.y < 300) // new Vector2가 중복으로 더해지는 것을 막는 코드 -> 씬 이동에서 발생
            {
                itemPanel.transform.position = mypos + new Vector2(50, 150);
            }
            itemPanel.SetActive(true);
        }
    }
    public void usingItem() // 아이템 사용 버튼 이벤트
    {
        ItemPanelOff(); // 아이템 선택지 패널 닫고
        um.InventoryOff(); // 인벤토리 닫고
        um.IsUIOn = false; // 터치 가능하게 열어주고
        NowState.SetActive(true); // 상태 텍스트 표시
        NowState.GetComponent<Text>().text = "아이템 사용 : " + selecteditem.name; // 메세지 내용 변경
    }


    public void destroyItem() // 아이템 분해
    {

    }


}