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

    static bool flag_firstdialogue;

    bool isNextScene;
    bool isLastScript;

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

        flag_firstdialogue = true; //최초 실행 대사에 쓸 플래그

        isNextScene = false;
        isLastScript = false;

        if (inventory.FindItem("voodooDoll"))
        {
            GameObject.Find("voodooDoll").SetActive(false);
        }

        theDM = FindObjectOfType<DialogueManager>();

        getdialog = FindObjectOfType<InteractionEvent>();


        
        /*if (inventory.FindItem("Hammer") == false && NowState.activeSelf == false)
        {
            ScriptStart(1, 7); //대사 : 제단방 진입
        }*/
    }
    void Update()
    {
        /*if (flag_firstdialogue)
        {
            flag_firstdialogue = false;
            ScriptStart(1, 7); //대사 : 제단방 진입
        }*/

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
                        ScriptStart(64, 70); //대사 : 인형 습득
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        //um.NewItemAddPanelOn("아이템 획득 : 부두 인형");
                        flag_Hammer = true;
                        Debug.Log(2);
                    }
                    else if(NowState.activeSelf == false)
                    {
                        flag_doll = true;
                        ScriptStart(55, 63);  //대사 : 인형 관찰
                        //Debug.Log(1);
                    }
                    else
                    {
                        ScriptStart(71, 71); // 사용불가
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Candle1" || clikedObj.name == "Candle2") 
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(35, 41);// 대사 출력 : 켜져있는 촛불 -> 누군가 왔다 간지 얼마 안됐다.
                        flag_candle = true;
                    }
                    else
                    {
                        ScriptStart(71, 71); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoChild") // 어린 아이 사진
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(91, 97); // 대사 출력 : 어린 아이와 알 수 없는 누군가의 사진이다.
                        flag_Cphoto = true;
                    }
                    else
                    {
                        ScriptStart(71, 71); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoAdult") // 제단에 놓인 8인의 사진
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(8, 34); // 대사 출력 : 영정사진~~~
                        flag_Aphoto = true;
                    }
                    else
                    {
                        ScriptStart(71, 71); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CCTV") // CCTV 
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(42, 54); // 대사 출력 : CCTV....
                        flag_CCTV = true;
                    }
                    else
                    {
                        ScriptStart(71, 71); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorriderDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        if (inventory.FindItem("voodooDoll"))
                        {
                            SceneManager.LoadScene("5F_Corridor");
                        }
                        else if ((flag_Aphoto && flag_candle && flag_CCTV && flag_Cphoto && flag_doll)||flag_Hammer)
                        { // 모든 오브젝트 조사 완료 시
                            EndScriptStart(79, 90); // 대사 출력 : 다시 한 번 문을 열어봄 -> 문이 열림 -> 주시하고 있다?
                            //SceneManager.LoadScene("5F_Corridor");
                        }
                        else
                        {
                            ScriptStart(73, 78); // 대사 아직 작성 X  // 대사 출력 : 아직 조사할 게 남아 있다.
                        }
                    }
                    else
                    {
                        ScriptStart(71, 71); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "StartRoomDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(98, 102);  // 대사 출력 : 돌아갈 이유가 없다.
                    }
                    else 
                    {
                        ScriptStart(71, 71); // 사용불가
                    }
                    NowStateMsgCheck();
                }
            }
        }
        if (isLastScript)
        {
            if (theDM.isDialogue == false)
            {
                SceneManager.LoadScene("5F_Corridor");
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

    void ScriptStart(int Start_num, int End_num)
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

    }

    void EndScriptStart(int Start_num, int End_num)
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

        isLastScript = true;
    }
}

