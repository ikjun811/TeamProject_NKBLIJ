using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Touch_Panel_CandleRoom : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public GameObject ip;
    public GameObject NowState;
    public GameObject NowLocate;
    public GameObject Canvas;
    private GameObject clikedObj;

    DialogueManager theDM;
    InteractionEvent getdialog;

    public GameObject DoorLockPanel;
    private bool flag_doll, flag_candle, flag_Aphoto, flag_Cphoto, flag_CCTV, flag_Hammer;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.Find("Inventory").transform.Find("InventoryPanel").transform.Find("ItemPanel").gameObject;
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");

        NowLocate.GetComponent<Text>().text = "현재 위치 : 추모의 방";
        clikedObj = null;

        flag_doll = false;
        flag_Cphoto = false;
        flag_CCTV = false;
        flag_candle = false;
        flag_Aphoto = false;
        flag_Hammer = false;
        if (inventory.FindItem("voodooDoll"))
        {
            GameObject.Find("voodooDoll").SetActive(false);
        }

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
                if (clikedObj.name == "voodooDoll")
                {
                    Debug.Log(clikedObj.name);
                    if (inventory.FindItem("Hammer") && NowState.activeSelf == false) // 해머가 있을 때 = 복도 다녀옴
                    {
                        StartCoroutine(ScriptStart(54, 59)); //대사 : 인형 습득
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        //um.NewItemAddPanelOn("아이템 획득 : 부두 인형");
                        flag_Hammer = true;
                        Debug.Log(2);
                    }
                    else if(NowState.activeSelf == false)
                    {
                        flag_doll = true;
                        StartCoroutine(ScriptStart(45, 53));  //대사 : 인형 관찰
                        //Debug.Log(1);
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(60, 60)); // 사용불가
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Candle1" || clikedObj.name == "Candle2") 
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(33, 37));// 대사 출력 : 켜져있는 촛불 -> 누군가 왔다 간지 얼마 안됐다.
                        flag_candle = true;
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(60, 60)); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoChild") // 어린 아이 사진
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(79, 79)); // 대사 출력 : 어린 아이와 알 수 없는 누군가의 사진이다.
                        flag_Cphoto = true;
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(60, 60)); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoAdult") // 제단에 놓인 8인의 사진
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(8, 32)); // 사용불가// 대사 출력 : 영정사진~~~
                        flag_Aphoto = true;
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(60, 60)); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CCTV") // CCTV 
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(38, 44)); // 사용불가// 대사 출력 : CCTV....
                        flag_CCTV = true;
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(60, 60)); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorriderDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        if ((flag_Aphoto && flag_candle && flag_CCTV && flag_Cphoto && flag_doll)||flag_Hammer)
                        { // 모든 오브젝트 조사 완료 시
                            StartCoroutine(ScriptStart(67, 78)); // 대사 출력 : 다시 한 번 문을 열어봄 -> 문이 열림 -> 주시하고 있다?
                            SceneManager.LoadScene("5F_Corridor");
                        }
                        else
                        {
                            StartCoroutine(ScriptStart(60, 60)); // 대사 아직 작성 X  // 대사 출력 : 아직 조사할 게 남아 있다.
                        }
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(60, 60)); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "StartRoomDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // 대사 출력 : 돌아갈 이유가 없다.
                    }
                    else 
                    {
                        StartCoroutine(ScriptStart(60, 60)); // 사용불가
                    }
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

