using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchPanel_ButtonRoom : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public GameObject ip; // itemPanel �� ���� ��ũ��Ʈ ������
    public ItemPanel itempanel;
    public GameObject NowState; // ����� Text
    public GameObject NowLocate;
    public GameObject Canvas;
    private GameObject clikedObj;

    public GameObject DoorLockPanel;

    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.Find("Inventory").transform.Find("InventoryPanel").transform.Find("ItemPanel").gameObject;
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");
        NowLocate.GetComponent<Text>().text = "���� ��ġ : ��ư�� ��";
        clikedObj = null;

        itempanel = ip.GetComponent<ItemPanel>();
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
                        // ��� ��� = ���� �ǹ� ������� ��︮�� �ʴ� �� �� ���� �������̴�...
                    }
                    else // ������ ��� ���� ���� 
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                        return;
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonRoomDoor") // ��ư�� �� ����
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� : �ڵ����� �� ����. �� ���� � ��ġ�� �ִ� �� ������..
                    }
                    else
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "FingerPrintReader") // ����������
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� : ���� �νı��̴�. �츮 ��� �հ����� �÷� �������� �۵����� �ʾҴ�...
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "voodooDoll")
                        {
                            // ��� ��� : ������ ������ �Է����� ���� ����
                            SceneManager.LoadScene("5F_ButtonRoom");
                        }
                        else
                        {
                            um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hammer") // ��ġ ����
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� : ��ġ�׿�. ì�ܵθ� ���� ���� �� ���ƿ�....
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("������ ȹ�� : ��ġ");
                    }
                    else
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "LockedDoor")
                {
                    Debug.Log(clikedObj.name);
                    if (NowState.activeSelf == false)
                    {
                        // ��� ��� : �Ʒ������� ������ �� �ִ� �� ���� ���̴�. ������ ����־� �� �� ����.
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        Debug.Log(tempItemName);
                        if (tempItemName == "5F_Key")
                        {
                            //  ��� ��� : ��� ���� �������� �Ҹ��� �Բ� ���� ���ȴ�...
                            SceneManager.LoadScene("4F");
                        }
                        else
                        {
                            um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
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
}

