using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Touch_Panel : MonoBehaviour
{
    private UIManager um;
    private GameObject clikedObj;
    public Inventory inventory;
    public Text NewItemAddText;
    public ItemPanelOnOff ipoo; // itemPanel 에 붙은 스크립트 가져옴
    public GameObject NowState; // 사용중 Text
    public GameObject NowLocate;

    private bool flag_Lighter;
    private bool flag_Gas;
    public GameObject DoorLockPanel;
    private void Start()
    {
        NowLocate.GetComponent<Text>().text = "현재 위치 : 시작의 방";
        clikedObj = null;
        um = GameObject.Find("UIManager").GetComponent<UIManager>();
        flag_Lighter = false;
        flag_Gas = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !um.IsUIOn)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                clikedObj = hit.transform.gameObject;
                if (clikedObj.name == "Lighter")
                {
                    // 대사 출력
                    NewItemAddText.GetComponent<Text>().text = "아이템 획득 : 라이터";
                    inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                    
                    Destroy(clikedObj);
                    um.NewItemAddPanelOn();
                    flag_Lighter = true;
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Gas")
                {
                    // 대사 출력
                    NewItemAddText.GetComponent<Text>().text = "아이템 획득 : 라이터 기름";
                    inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                    Destroy(clikedObj);
                    um.NewItemAddPanelOn();
                    flag_Gas = true;
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DoorLock")
                {
                    string mystr = ipoo.getItem();
                    if (flag_Gas == true && flag_Lighter == true && mystr == "Lighter" && NowState.activeSelf == true) // 분해 구현 시, Box & Gas 구분 + 조합된 아이템 구분
                    {
                        DoorLockPanelOn();

                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Book")
                {
                    // 대사 출력
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Window")
                {
                    // 대사 출력
                    NowStateMsgCheck();
                }
            }
        }
    }
        public void DoorLockPanelOn()
    {
        um.IsUIOn = true;
        DoorLockPanel.SetActive(true);
    }
    public void DoorLockPanelOff()
    {
        DoorLockPanel.SetActive(false);
        um.IsUIOn = false;
    }
    void NowStateMsgCheck()
    {
        if (NowState.activeSelf == true)
        {
            NowState.SetActive(false);
        }
    }
}
