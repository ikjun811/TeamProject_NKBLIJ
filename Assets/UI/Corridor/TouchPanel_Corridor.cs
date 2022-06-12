using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchPanel_Corridor : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public GameObject ip; // itemPanel �� ���� ��ũ��Ʈ ������
    public ItemPanel itempanel;
    public GameObject NowState; // ����� Text
    public GameObject NowLocate;
    public GameObject Canvas;
    private GameObject clikedObj;

    DialogueManager theDM;
    InteractionEvent getdialog;

    public GameObject DoorLockPanel;
    private bool flag_light, flag_LockedDoor, flag_ButtonRoomDoor, flag_FingerPrintReader;
    private bool flag_Hammer, flag_voodooDoll;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.Find("Inventory").transform.Find("InventoryPanel").transform.Find("ItemPanel").gameObject;
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");
        NowLocate.GetComponent<Text>().text = "���� ��ġ : ����";
        clikedObj = null;

        itempanel = ip.GetComponent<ItemPanel>();

        flag_light = false;
        flag_LockedDoor = false;
        flag_ButtonRoomDoor = false;
        flag_FingerPrintReader = false;
        flag_Hammer = false;
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
                    if (NowState.activeSelf == false) // ����Ʈ ����
                    {
                        StartCoroutine(ScriptStart(1, 6)); // ��� ��� = ���� �ǹ� ������� ��︮�� �ʴ� �� �� ���� �������̴�...
                        flag_light = true;
                    }
                    else // ������ ��� ���� ���� 
                    {
                        StartCoroutine(ScriptStart(50, 50)); // ���Ұ�
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonRoomDoor") // ��ư�� �� ����
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(7, 12)); // ��� ��� : �ڵ����� �� ����. �� ���� � ��ġ�� �ִ� �� ������..
                        flag_ButtonRoomDoor = true;
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(50, 50)); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "FingerPrintReader") // ����������
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(13, 25)); // ��� ��� : ���� �νı��̴�. �츮 ��� �հ����� �÷� �������� �۵����� �ʾҴ�...
                        flag_FingerPrintReader = true;
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "voodooDoll")
                        {
                            inventory.RemoveItem("voodooDoll");
                            StartCoroutine(ScriptStart(26, 28)); // ��� ��� : ������ ������ �Է����� ���� ����
                            SceneManager.LoadScene("5F_ButtonRoom");
                        }
                        else
                        {
                            StartCoroutine(ScriptStart(50, 50)); // ���Ұ�
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hammer") // ��ġ ����
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(30, 34)); //��� ��� : ��ġ�׿�. ì�ܵθ� ���� ���� �� ���ƿ�....
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        //um.NewItemAddPanelOn("������ ȹ�� : ��ġ");
                        flag_Hammer = true;
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(50, 50)); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CandleRoomDoor") // CCTV 
                {
                    Debug.Log(clikedObj.name);
                    if (inventory.FindItem("voodooDoll"))
                    {
                        StartCoroutine(ScriptStart(35,37)); // ���� ȹ���� ���ư� ������ ����
                    }
                    else if (NowState.activeSelf == false && flag_ButtonRoomDoor && flag_FingerPrintReader && flag_light && flag_LockedDoor && flag_Hammer)
                    {
                        StartCoroutine(ScriptStart(38,38)); // ��� ��� : (����) ������ �� �ִ� �� �� �ô�. ���� ������ ���ư���. �Ʊ��� ������ �Ű� ���δ�.....
                        SceneManager.LoadScene("5F_CandleRoom");
                    }
                    else if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(52, 52)); // ��� ��� : ������ ���ư� ������ ���� �� ����.
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(50, 50)); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "LockedDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        StartCoroutine(ScriptStart(40, 46)); // ��� ��� : �Ʒ������� ������ �� �ִ� �� ���� ���̴�. ������ ����־� �� �� ����.
                        flag_LockedDoor = true;
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        Debug.Log(tempItemName);
                        if (tempItemName == "5F_Key")
                        {
                            StartCoroutine(ScriptStart(47,48)); // ���Ұ� //  ��� ��� : ��� ���� �������� �Ҹ��� �Բ� ���� ���ȴ�...
                            SceneManager.LoadScene("4F");
                        }
                        else
                        {
                            StartCoroutine(ScriptStart(50, 50)); // ���Ұ�
                        }
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

