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

    public GameObject redButtonCase;
    public GameObject blueButtonCase;
    public Sprite redButtonSprite;
    public Sprite blueButtonSprite;
    private bool flag_redBtn;
    private bool flag_blueBtn;
    
    private void Start()
    {
        um = GameObject.FindObjectOfType<UIManager>();
        inventory = GameObject.FindObjectOfType<Inventory>();
        ip = GameObject.Find("Inventory").transform.Find("InventoryPanel").transform.Find("ItemPanel").gameObject;
        itempanel = ip.GetComponent<ItemPanel>();
        NowState = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        NowLocate = GameObject.Find("NowLocateText");
        NowLocate.GetComponent<Text>().text = "���� ��ġ : ��ư�� ��";
        clikedObj = null;

        redButtonCase.GetComponent<BoxCollider2D>().enabled = false;
        blueButtonCase.GetComponent<BoxCollider2D>().enabled = false;
        flag_redBtn = false;
        flag_blueBtn = false;
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
                if (clikedObj.name == "KeyRoomDoor") // ��ư �÷��� 2�� �߰�
                {
                    if (NowState.activeSelf == false && (!flag_blueBtn || !flag_redBtn))
                    {
                        Debug.Log("���������� ���� ���̴�. �����̵� ����... Ʈ���� �ִ� �� ����.");
                    }
                    else if (NowState.activeSelf == false && flag_redBtn && flag_blueBtn)
                    {
                        Debug.Log("��ư�� ���ÿ� ������ ���� ���� ������ ���� ���ȴ�...");
                        SceneManager.LoadScene("5F_KeyRoom");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorridorDoor") // ��ư�� �� ����
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("��� ��� : ���ư� ������ ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "RedButton" || clikedObj.name == "BlueButton") // ��ư ����
                {
                    if (NowState.activeSelf == false) // �� ��� �� �ƴ� ��.
                    {   // ���� �ʿ� ���� ��
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        um.NewItemAddPanelOn("������ ȹ�� : " + clikedObj.name);
                    }
                    else
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "WoodRed" || clikedObj.name == "WoodBlue") // ��������
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("��� ��� : ưư�غ��̴� ���������̴�. �ȿ� ��ġ������ �����ִ�... �ν��� �� �� ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "Hammer")
                        {
                            // ��� ��� : ��ġ�� �ֵθ��� ���� ���ڰ� �μ�����~~~
                            Destroy(clikedObj);
                            if (clikedObj.name == "WoodRed")
                            {
                                redButtonCase.GetComponent<BoxCollider2D>().enabled = true;
                            }
                            else if (clikedObj.name == "WoodBlue")
                            {
                                blueButtonCase.GetComponent<BoxCollider2D>().enabled = true;
                            }
                        }
                        else
                        {
                            um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonCaseRed" || clikedObj.name == "ButtonCaseBlue") // ��ư ����
                {
                    if (NowState.activeSelf == false)
                    {
                        if (clikedObj.name == "ButtonCaseRed" && flag_redBtn == true)
                        {
                            Debug.Log("��� ��� : ��ġ�� ���� ��ư�� ���� �ִ�.");
                        }
                        else if(clikedObj.name == "ButtonCaseBlue" && flag_blueBtn == true)
                        {
                            Debug.Log("��� ��� : ��ġ�� �Ķ� ��ư�� ���� �ִ�.");
                        }
                        else
                        {
                            Debug.Log("��� ��� : ���ڸ� �μ��� �ȿ� ��� ��ġ�� ����� �巯�´�... ���𰡸� �Ⱦƾ� �� �� ����...");
                        }
                    }
                    else // ������ ��� ���� ���� 
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "RedButton" && clikedObj.name == "ButtonCaseRed")
                        {
                            Debug.Log("��� ��� : ��Ĭ �Ҹ��� ���� ���� ��ư�� �¹��� �����ƴ�.");
                            inventory.RemoveItem("RedButton");
                            redButtonCase.GetComponent<SpriteRenderer>().enabled = true;
                            redButtonCase.GetComponent<SpriteRenderer>().sprite = redButtonSprite;
                            flag_redBtn = true;
                        }
                        else if (tempItemName == "BlueButton" && clikedObj.name == "ButtonCaseBlue")
                        {
                            Debug.Log("��� ��� : ��Ĭ �Ҹ��� ���� �Ķ� ��ư�� �¹��� �����ƴ�.");
                            inventory.RemoveItem("BlueButton");
                            blueButtonCase.GetComponent<SpriteRenderer>().enabled = true;
                            blueButtonCase.GetComponent<SpriteRenderer>().sprite = blueButtonSprite;
                            flag_blueBtn = true;
                        }
                        else
                        {
                            um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "RedCircle" || clikedObj.name == "BlueCircle") // ���� ��
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("��� ��� : ���� ���� ���� ĥ���� ���� �׷��� �ִ�. -> ��ư ���� ��Ʈ.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hat") // ������
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("��� ��� : ������ ��� ������ �� ���ƺ��� �������̴�. �� ������ �ȵ� �� ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Trash") // ���������
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("��� ��� : ���� �Ǽ�������� �׿� �ִ�. �˴� ���� �콽�� �Ժη� ������ �ȵ� �� ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Newspaper") // �Ź���
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("��� ��� : �� ���� �� ��¥�� �Ź��� ������ �ִ�. -> �Ǽ��� ���� ���� ���");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DrumCement") // �巳��+�ø�Ʈ����
                {
                    if (NowState.activeSelf == false)
                    {
                        Debug.Log("��� ��� : �巳��� �ø�Ʈ ���밡 ���� �ִ�. ... ū ����� ���� �� ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        um.NewItemAddPanelOn("����� �� ���� �� ����."); // UI ���, ��� ó�� �ʿ�
                    }
                    NowStateMsgCheck();
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
}

