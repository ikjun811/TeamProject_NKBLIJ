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
    public ItemPanel ip; // itemPanel �� ���� ��ũ��Ʈ ������
    public GameObject NowState; // ����� Text
    public GameObject NowLocate;

    DialogueManager theDM;

    InteractionEvent getdialog;

    public GameObject DoorLockPanel;

    private void Start()
    {
        NowLocate.GetComponent<Text>().text = "���� ��ġ : ������ ��";
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
                        // ��� ���
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        StartCoroutine(ScriptStart(39, 45));
                        //um.NewItemAddPanelOn("������ ȹ�� : ���� ���� ������");
                       // NowStateMsgCheck();
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(81, 81));
                        //um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Box")
                {
                    // ��� ���
                    StartCoroutine(ScriptStart(25, 30));
                    if (NowState.activeSelf == false)
                    {
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        //um.NewItemAddPanelOn("������ ȹ�� : ����");
                        NowStateMsgCheck();
                    }
                    else
                    {
                        StartCoroutine(ScriptStart(81, 81));
                        //um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DoorLock")
                {
                    string tempItemName = ip.getItem();
                    if (tempItemName == "Lighter_F" && NowState.activeSelf == true)
                    {  // ���� ���� ��, ����
                        DoorLockPanelOn();
                    }
                    else if (tempItemName != "Lighter_F" && NowState.activeSelf == true)
                    {  // ������ �� ������������ ������� �ٸ� �������� ������� ��
                        StartCoroutine(ScriptStart(81, 81));
                        //um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    else // ���� ������
                    {
                        StartCoroutine(ScriptStart(20, 24));
                        // �Ϲ� ���â -> ������̴� -> ��й�ȣ�� �Է��ؾ� ���� �� ���� �� ����...
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Book")
                {
                    if (NowState.activeSelf == true)
                    {
                        StartCoroutine(ScriptStart(81, 81));
                        //um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    StartCoroutine(ScriptStart(1, 19));// ��� ���
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Window")
                {
                    if (NowState.activeSelf == true)
                    {
                        StartCoroutine(ScriptStart(83, 88));
                        //um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    // ��� ���
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
