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
    public ItemPanel ip; // itemPanel 에 붙은 스크립트 가져옴
    public GameObject NowState; // 사용중 Text
    public GameObject NowLocate;

    DialogueManager theDM;

    InteractionEvent getdialog;

    public GameObject DoorLockPanel;

    private void Start()
    {
        NowLocate.GetComponent<Text>().text = "현재 위치 : 시작의 방";
        clikedObj = null;
        um = GameObject.Find("UIManager").GetComponent<UIManager>();

        theDM = FindObjectOfType<DialogueManager>();

        getdialog = FindObjectOfType<InteractionEvent>();
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
                   
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        StartCoroutine(ScriptStart(39, 45));
                        //um.NewItemAddPanelOn("아이템 획득 : 가스 없는 라이터");
                       // NowStateMsgCheck();
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(81, 81));
                        //um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Box")
                {
                    // 대사 출력
                    StartCoroutine(ScriptStart(25, 30));
                    if (NowState.activeSelf == false)
                    {
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        //um.NewItemAddPanelOn("아이템 획득 : 상자");
                        NowStateMsgCheck();
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(81, 81));
                        //um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DoorLock")
                {
                    string tempItemName = ip.getItem();
                    if (tempItemName == "Lighter_F" && NowState.activeSelf == true)
                    {  // 조건 충족 시, 실행
                        DoorLockPanelOn();
                    }
                    else if (tempItemName != "Lighter_F" && NowState.activeSelf == true)
                    {  // 조건은 다 만족시켰지만 도어락에 다른 아이템을 사용했을 때
                        StartCoroutine(ScriptStart(81, 81));
                        //um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    else // 조건 미충족
                    {
                        StartCoroutine(ScriptStart(20, 24));
                        // 일반 대사창 -> 도어록이다 -> 비밀번호를 입력해야 나갈 수 있을 것 같다...
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Book")
                {
                    if (NowState.activeSelf == true)
                    {
                        StartCoroutine(ScriptStart(81, 81));
                        //um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
                    StartCoroutine(ScriptStart(1, 19));// 대사 출력
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Window")
                {
                    if (NowState.activeSelf == true)
                    {
                        StartCoroutine(ScriptStart(83, 88));
                        //um.NewItemAddPanelOn("사용할 수 없는 것 같다."); // UI 대신, 대사 처리 필요
                    }
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

    IEnumerator ScriptStart(int Start_num, int End_num)
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);
        yield return null;
    }
    
}
