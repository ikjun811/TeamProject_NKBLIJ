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
    public ItemPanelOnOff ipoo; // itemPanel �� ���� ��ũ��Ʈ ������
    public GameObject NowState; // ����� Text
    public GameObject NowLocate;

    private bool flag_Lighter;
    private bool flag_Gas;
    public GameObject DoorLockPanel;
    private void Start()
    {
        NowLocate.GetComponent<Text>().text = "���� ��ġ : ������ ��";
        clikedObj = null;
        um = GameObject.Find("UIManager").GetComponent<UIManager>();
        flag_Lighter = false;
        flag_Gas = false;
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
                if (clikedObj.name == "������")
                {
                    if (NowState.activeSelf == false)
                    {
                        // ��� ���
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("������ ȹ�� : ���� ���� ������");
                        flag_Lighter = true;
                        NowStateMsgCheck();
                    }
                    else
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "����")
                {
                    // ��� ���
                    if (NowState.activeSelf == false)
                    {
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("������ ȹ�� : ����");
                        flag_Gas = true;
                        NowStateMsgCheck();
                    }
                    else
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DoorLock")
                {
                    // bool temp = inventory.FindItem("Gas");  ���� ��� ���� �ϼ� ����
                    string tempItemName = ipoo.getItem();
                    if (flag_Gas == true && flag_Lighter == true && tempItemName == "Lighter" && NowState.activeSelf == true) // ���� ���� ��, Box & Gas ���� + ���յ� ������ ����
                    {  // ���� ���� ��, ����
                        DoorLockPanelOn();
                    }
                    else if (flag_Gas == true && flag_Lighter == true && tempItemName != "Lighter" && NowState.activeSelf == true) // ���� ���� ��, Box & Gas ���� + ���յ� ������ ����
                    {  // ������ �� ������������ ������� �ٸ� �������� ������� �� : �߻� ���ɼ��� ����
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    else // ���� ������
                    {
                        um.NewItemAddPanelOn("���� �����"); // UI ���, ��� ó�� �ʿ�
                        // ��� ���
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Book")
                {
                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    // ��� ���
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Window")
                {
                    if (NowState.activeSelf == true)
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
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
}
