using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchPanel_KeyRoom : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public GameObject ip; // itemPanel 에 붙은 스크립트 가져옴
    public ItemPanel itempanel;
    public GameObject NowState; // 사용중 Text
    public GameObject NowLocate;
    public GameObject Canvas;
    private GameObject clikedObj;

    private bool keyTouchFlag;
    private bool FloorFlag;
    public GameObject floor;

    DialogueManager theDM;
    InteractionEvent getdialog;

    bool isLastScript;

    bool isGameover;
    bool isKeyGet;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.Find("Inventory").transform.Find("InventoryPanel").transform.Find("ItemPanel").gameObject;
        itempanel = ip.GetComponent<ItemPanel>();
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");
        NowLocate.GetComponent<Text>().text = "현재 위치 : 열쇠의 방";
        clikedObj = null;

        keyTouchFlag = false;
        FloorFlag = false;
        floor.GetComponent<SpriteRenderer>().enabled = false;

        theDM = FindObjectOfType<DialogueManager>();

        getdialog = FindObjectOfType<InteractionEvent>();

        isLastScript = false;

        isGameover = false;
        isKeyGet = false;
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
                if (clikedObj.name == "Key") // 버튼룸 문 조사
                {
                    if (NowState.activeSelf == false)
                    {  // 사망 씬 이전, 1회 경고
                        if (FloorFlag == true)
                        {
                            isKeyGet = true;
                            EndScriptStart(3, 16); //열쇠 획득 & 복도 귀환
                            Debug.Log("대사 출력 : 꺼진 바닥을 피해 벽에 붙어 조심조심 키를 습득하는 과정 대사");
                            inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                            Destroy(clikedObj);
                            //um.NewItemAddPanelOn("아이템 획득 : " + clikedObj.name);
                            Debug.Log("대사 출력 : 아마 이것이 이 층을 통과하는 열쇠일 것이다... 복도로 돌아가자...");
                            //SceneManager.LoadScene("5F_Corridor");
                        }
                        else if (keyTouchFlag == false)
                        {
                            ScriptStart(17, 26); //열쇠 1차 상호작용
                            Debug.Log("대사 출력 : 저쪽에 키가 보인다. 잠깐 아무래도 뭔가 수상한데...");
                            keyTouchFlag = true;
                        }
                        else if (keyTouchFlag == true)
                        {
                            isGameover = true;
                            EndScriptStart(27, 32); //열쇠2트 게임오버
                            Debug.Log("대사 출력 : 발을 내딛는 순간, 바닥이 사라지더니 아래로 곤두박질 치기 시작했다....");
                            //SceneManager.LoadScene("Fail");
                        }
                    }
                    else // 아이템 사용 중인 상태 
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "IronBall") // 강철공 습득
                {
                    if (NowState.activeSelf == false) // 템 사용 중 아닐 때.
                    {
                        ScriptStart(33, 39); //강철공 획득
                        Debug.Log("대사 출력 : 바닥에 강철공이 놓여 있다. 아무 의미없이 여기 있을 것 같진 않은데...");
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        //um.NewItemAddPanelOn("아이템 획득 : " + clikedObj.name);
                    }
                    else
                    {
                        ScriptStart(1, 1); // 사용불가
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Floor") // 바닥 조사
                {
                    if (NowState.activeSelf == false && FloorFlag == false) // 템 사용 중 아닐 때.
                    {
                        ScriptStart(40, 49); //바닥 확인
                        Debug.Log("대사 출력 : 바닥을 두드려보았다. 텅 빈 소리? -> 함정인가?");
                    }
                    else if (NowState.activeSelf == false && FloorFlag == true)
                    {
                        ScriptStart(61, 63); //부서진 바닥 확인
                        Debug.Log("대사 출력 : 바닥 사라짐 -> 까만 바닥");
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "IronBall")
                        {
                            ScriptStart(50, 60); //
                            Debug.Log("대사 출력 : 바닥에 강철공을 던졌다 -> 바닥이 꺼졌다.");
                            floor.GetComponent<SpriteRenderer>().enabled = true;
                            inventory.RemoveItem("IronBall");
                            FloorFlag = true;
                        }
                        else
                        {
                            ScriptStart(1, 1); //사용불가
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
                if (isGameover)
                {
                    SceneManager.LoadScene("Fail");
                }
                else if (isKeyGet)
                {
                    SceneManager.LoadScene("5F_Corridor");
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

    void ScriptStart(int Start_num, int End_num) //대사 호출
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

    }

    void EndScriptStart(int Start_num, int End_num) // 씬 이동 이전 대사 호출
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

        isLastScript = true;
    }
}

