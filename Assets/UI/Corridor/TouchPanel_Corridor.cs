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

        NowLocate.GetComponent<Text>().text = "���� ��ġ : ����";
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
                    if (NowState.activeSelf == false) // ����Ʈ ����
                    {
                        ScriptStart(1, 7); // ��� ��� = ���� �ǹ� ������� ��︮�� �ʴ� �� �� ���� �������̴�...
                        flag_light = true;
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(52, 52); // ���Ұ�
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonRoomDoor") // ��ư�� �� ����
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(8, 13); // ��� ��� : �ڵ����� �� ����. �� ���� � ��ġ�� �ִ� �� ������..
                        flag_ButtonRoomDoor = true;
                    }
                    else
                    {
                        ScriptStart(52, 52); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "FingerPrintReader") // ����������
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(14, 24); // ��� ��� : ���� �νı��̴�. �츮 ��� �հ����� �÷� �������� �۵����� �ʾҴ�...
                        flag_FingerPrintReader = true;
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "voodooDoll")
                        {
                            inventory.RemoveItem("voodooDoll");
                            EndScriptStartForButtonroom(25, 28); // ��� ��� : ������ ������ �Է����� ���� ����
                        }
                        else
                        {
                            ScriptStart(52, 52); // ���Ұ�
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hammer") // ��ġ ����
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(29, 32); //��� ��� : ��ġ�׿�. ì�ܵθ� ���� ���� �� ���ƿ�....
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item); //��ġ ȹ��
                        Destroy(clikedObj);
                        flag_Hammer = true;
                    }
                    else
                    {
                        ScriptStart(52, 52); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CandleRoomDoor") // CCTV 
                {
                    Debug.Log(clikedObj.name);
                    if (inventory.FindItem("voodooDoll"))
                    {
                        ScriptStart(33, 35); // ���� ȹ���� ���ư� ������ ����
                    }
                    else if (NowState.activeSelf == false && flag_ButtonRoomDoor && flag_FingerPrintReader && flag_light && flag_LockedDoor && flag_Hammer)
                    {
                        SceneManager.LoadScene("5F_CandleRoom");
                    }
                    else if (NowState.activeSelf == false)
                    {
                        ScriptStart(37, 37);  // ��� ��� : ������ ���ư� ������ ���� �� ����.
                    }
                    else
                    {
                        ScriptStart(52, 52); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "LockedDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(39, 46); // ��� ��� : �Ʒ������� ������ �� �ִ� �� ���� ���̴�. ������ ����־� �� �� ����.
                        flag_LockedDoor = true;
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        Debug.Log(tempItemName);
                        if (tempItemName == "5F_Key")
                        {
                            EndScriptStartFor4F(47, 51); //  ��� ��� : ��� ���� �������� �Ҹ��� �Բ� ���� ���ȴ�...
                        }
                        else
                        {
                            ScriptStart(52, 52); // ���Ұ�
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

