using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_Panel_CandleRoom : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public ItemPanel ip; // itemPanel �� ���� ��ũ��Ʈ ������
    public GameObject NowState; // ����� Text
    public GameObject NowLocate;
    public GameObject Canvas;
    private GameObject clikedObj;

    public GameObject DoorLockPanel;
    private bool flag_doll, flag_candle, flag_Aphoto, flag_Cphoto, flag_CCTV;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.FindObjectOfType<ItemPanel>();
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");

        NowLocate.GetComponent<Text>().text = "���� ��ġ : �߸��� ��";
        clikedObj = null;

        flag_doll = false;
        flag_candle = false;
        flag_Aphoto = false;
        flag_Cphoto = false;
        flag_CCTV = false;

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
                    if (NowState.activeSelf == false) // �����x + �����ٳ���� ��
                    {
                        // �ε������̹��� ��ưȭ = ������ ��� ���
                        // ��� ��� : �ұ��� ������ �����̴�..., ������ �ȴ�... 
                        flag_doll = true;
                    }
                    //else if (NowState.active == false ) ���� �ٳ�� �� : ������ ���� ����
                    //inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                    //Destroy(clikedObj);
                    //um.NewItemAddPanelOn("������ ȹ�� : �ε� ����");
                    else // ������ ������ ����Ϸ� �� ���
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Candle1" || clikedObj.name == "Candle2") // �к� ������ ��
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� = �к��̳׿�... ��� ���� �ҵ��� -> ������ ������ �־���
                        flag_candle = true;
                    }
                    else // �кҿ� ������ ����Ϸ� �� ���
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoChild") // ���� ���� ����
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� : ���� ���̿� �� �� ���� �ι��� �����̴�...
                        flag_Cphoto = true;
                    }
                    else // ������ ������ ��� ��.
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "PhotoAdult")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� : ���� ����~~~
                        flag_Aphoto = true;
                    }
                    else // ������ ������ ��� ��.
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CCTV")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� : CCTV ~~~
                        flag_Cphoto = true;
                    }
                    else // cctv�� ������ ��� ��.
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorriderDoor") // ���� ���� �ʿ�
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� :
                        flag_Cphoto = true;
                    }
                    else // cctv�� ������ ��� ��.
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "StartRoomDoor")
                {
                    Debug.Log(clikedObj.name);

                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    else
                    {
                        // ��� ���
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
}

