using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchPanel_Corridor : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public GameObject ip; // itemPanel 에 붙은 스크립트 가져옴
    public ItemPanel itempanel;
    public GameObject NowState; // 사용중 Text
    public GameObject NowLocate;
    private GameObject clikedObj;

    DialogueManager theDM;
    InteractionEvent getdialog;

    private bool flag_light, flag_LockedDoor, flag_ButtonRoomDoor, flag_FingerPrintReader;
    private bool flag_Hammer;

    bool isLastScript;
    bool isButtonRoom;
    bool is4F;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.Find("Inventory").transform.Find("InventoryPanel").transform.Find("ItemPanel").gameObject;
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");

        NowLocate.GetComponent<Text>().text = "현재 위치 : 복도";
        clikedObj = null;

        itempanel = ip.GetComponent<ItemPanel>();

        flag_light = false;
        flag_LockedDoor = false;
        flag_ButtonRoomDoor = false;
        flag_FingerPrintReader = false;
        flag_Hammer = false;


        isLastScript = false;

        isButtonRoom = false;
        is4F = false;

        if (inventory.FindItem("Hammer"))
        {
            GameObject.Find("Hammer").SetActive(false);
            flag_Hammer = true;
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
                if (clikedObj.name == "Light")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false) // 라이트 조사
                    {
                        ScriptStart(1, 7); // 대사 출력 = 낡은 건물 분위기와 어울리지 않는 새 것 같은 형광등이다...
                        flag_light = true;
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(52, 52); // 사용불가
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonRoomDoor") // 버튼룸 문 조사
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(8, 13); // 대사 출력 : 자동문인 것 같다. 문 옆에 어떤 장치가 있는 것 같은데..
                        flag_ButtonRoomDoor = true;
                    }
                    else
                    {
                        ScriptStart(52, 52); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "FingerPrintReader") // 지문리더기
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(14, 24); // 대사 출력 : 지문 인식기이다. 우리 모두 손가락을 올려 보았지만 작동하지 않았다...
                        flag_FingerPrintReader = true;
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "voodooDoll")
                        {
                            inventory.RemoveItem("voodooDoll");
                            EndScriptStartForButtonroom(25, 28); // 대사 출력 : 인형의 지문을 입력하자 문이 열림
                        }
                        else
                        {
                            ScriptStart(52, 52); // 사용불가
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hammer") // 망치 조사
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(29, 32); //대사 출력 : 망치네요. 챙겨두면 쓸모가 있을 것 같아요....
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item); //망치 획득
                        Destroy(clikedObj);
                        flag_Hammer = true;
                    }
                    else
                    {
                        ScriptStart(52, 52); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CandleRoomDoor") // CCTV 
                {
                    Debug.Log(clikedObj.name);
                    if (inventory.FindItem("voodooDoll"))
                    {
                        ScriptStart(33, 35); // 인형 획득후 돌아갈 이유가 없다
                    }
                    else if (NowState.activeSelf == false && flag_ButtonRoomDoor && flag_FingerPrintReader && flag_light && flag_LockedDoor && flag_Hammer)
                    {
                        SceneManager.LoadScene("5F_CandleRoom");
                    }
                    else if (NowState.activeSelf == false)
                    {
                        ScriptStart(37, 37);  // 대사 출력 : 지금은 돌아갈 이유가 없는 것 같다.
                    }
                    else
                    {
                        ScriptStart(52, 52); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "LockedDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(39, 46); // 대사 출력 : 아래층으로 내려갈 수 있는 것 같은 문이다. 지금은 잠겨있어 열 수 없다.
                        flag_LockedDoor = true;
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        Debug.Log(tempItemName);
                        if (tempItemName == "5F_Key")
                        {
                            EndScriptStartFor4F(47, 51); //  대사 출력 : 기분 나쁜 찢어지는 소리와 함께 문이 열렸다...
                        }
                        else
                        {
                            ScriptStart(52, 52); // 사용불가
                        }
                    }
                    NowStateMsgCheck();
                }
            }
        }

        if (isLastScript)
        {
            if (theDM.isDialogue == false)
            {
                if (isButtonRoom)
                {
                    SceneManager.LoadScene("5F_ButtonRoom");
                }
                else if (is4F)
                {
                    SceneManager.LoadScene("4F");
                }
            }
        }
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

    void EndScriptStartForButtonroom(int Start_num, int End_num)
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

        isButtonRoom = true;
        isLastScript = true;
    }

    void EndScriptStartFor4F(int Start_num, int End_num)
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

        is4F = true;
        isLastScript = true;
    }
}

