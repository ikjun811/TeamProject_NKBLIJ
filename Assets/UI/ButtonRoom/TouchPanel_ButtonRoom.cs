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
    private int count_wood;

    DialogueManager theDM;
    InteractionEvent getdialog;

    static bool flag_firstdialogue;

    bool isLastScript;

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
        count_wood = 0;

        theDM = FindObjectOfType<DialogueManager>();

        getdialog = FindObjectOfType<InteractionEvent>();

        flag_firstdialogue = true; //���� ���� ��翡 �� �÷���

        isLastScript = false;
    }
    void Update()
    {
        if (flag_firstdialogue)
        {
            flag_firstdialogue = false;
            ScriptStart(3, 19); //��� : ���ܹ� ����
        }

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
                        ScriptStart(42, 46); // �� ù ���� ���
                        Debug.Log("���������� ���� ���̴�. �����̵� ����... Ʈ���� �ִ� �� ����.");
                    }
                    else if (NowState.activeSelf == false && flag_redBtn && flag_blueBtn)
                    {
                        EndScriptStart(47, 63); //��ư�� ���ÿ� ������ ���� ����
                        Debug.Log("��ư�� ���ÿ� ������ ���� ���� ������ ���� ���ȴ�...");
                        //SceneManager.LoadScene("5F_KeyRoom");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "CorridorDoor") // ���� �� ����
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(20, 22); // ��� ��� ���ư� ������ ����.
                        Debug.Log("��� ��� : ���ư� ������ ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "RedButton" || clikedObj.name == "BlueButton") // ��ư ����
                {
                    if (NowState.activeSelf == false) // �� ��� �� �ƴ� ��.
                    {   // ���� �ʿ� ���� ��
                        if (clikedObj.name == "RedButton")
                        {
                            um.NewItemAddPanelOn("������ ȹ�� : ������ ��ư");
                        }
                        else if (clikedObj.name == "BlueButton")
                        {
                            um.NewItemAddPanelOn("������ ȹ�� : �Ķ��� ��ư");
                        }
                        inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                        Destroy(clikedObj);
                        //um.NewItemAddPanelOn("������ ȹ�� : " + clikedObj.name);
                    }
                    else
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "WoodRed" || clikedObj.name == "WoodBlue") // ��������
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(23, 28); //�������� ù ����
                        Debug.Log("��� ��� : ưư�غ��̴� ���������̴�. �ȿ� ��ġ������ �����ִ�... �ν��� �� �� ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "Hammer")
                        {
                            if (count_wood == 0) // ù°����
                            {
                                ScriptStart(29, 35); //ù �������� �ı�
                                Debug.Log("��ġ �ֵθ� -> ���� �μ����� �ȿ� ��ġ �߰� -> ��ġ ��û�Ÿ� -> ���� ���� �ϳ��� ���� �μ���");
                            }
                            else
                            {// �ι�°����
                                ScriptStart(36, 41); //�ι�° �������� �ı�
                                Debug.Log("���� �� �μ� -> ��ġ �μ���-> ������");
                            }
                            count_wood++;
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
                            ScriptStart(1, 1); // ���Ұ�
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "ButtonCaseRed" || clikedObj.name == "ButtonCaseBlue") // ��ư ����
                {
                    if (NowState.activeSelf == false)
                    {
                        if(flag_blueBtn && flag_redBtn)
                        {
                            ScriptStart(73, 78); // ��ư �ΰ� ��� �� ���
                            Debug.Log("��� ��� : �ΰ��� ��ġ�� ��� ��ư�� �Ⱦ� �־���. ���� ����");
                        }
                        else if (clikedObj.name == "ButtonCaseRed" && flag_redBtn == true)
                        {
                            ScriptStart(69, 69); //������ư ���� Ȯ��
                            Debug.Log("��� ��� : ��ġ�� ���� ��ư�� ���� �ִ�.");
                        }
                        else if(clikedObj.name == "ButtonCaseBlue" && flag_blueBtn == true)
                        {
                            ScriptStart(71, 71); //�Ķ���ư ���� Ȯ��
                            Debug.Log("��� ��� : ��ġ�� �Ķ� ��ư�� ���� �ִ�.");
                        }
                        else
                        {
                            ScriptStart(64, 68); //��ġ�� ���� ���� �� �����Ͱ���.
                            Debug.Log("��� ��� : ���ڸ� �μ��� �ȿ� ��� ��ġ�� ����� �巯�´�... ���𰡸� �Ⱦƾ� �� �� ����...");
                        }
                    }
                    else // ������ ��� ���� ���� 
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "RedButton" && clikedObj.name == "ButtonCaseRed")
                        {
                            ScriptStart(79, 83); //�����ư ����
                            Debug.Log("��� ��� : ��Ĭ �Ҹ��� ���� ���� ��ư�� �¹��� �����ƴ�.");
                            inventory.RemoveItem("RedButton");
                            redButtonCase.GetComponent<SpriteRenderer>().enabled = true;
                            redButtonCase.GetComponent<SpriteRenderer>().sprite = redButtonSprite;
                            flag_redBtn = true;
                        }
                        else if (tempItemName == "BlueButton" && clikedObj.name == "ButtonCaseBlue")
                        {
                            ScriptStart(84, 88); //����ư ����
                            Debug.Log("��� ��� : ��Ĭ �Ҹ��� ���� �Ķ� ��ư�� �¹��� �����ƴ�.");
                            inventory.RemoveItem("BlueButton");
                            blueButtonCase.GetComponent<SpriteRenderer>().enabled = true;
                            blueButtonCase.GetComponent<SpriteRenderer>().sprite = blueButtonSprite;
                            flag_blueBtn = true;
                        }
                        else
                        {
                            ScriptStart(1, 1); // ���Ұ�
                        }
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "RedCircle" || clikedObj.name == "BlueCircle") // ���� ��
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(89, 95); //���� �� Ȯ��
                        Debug.Log("��� ��� : ���� ���� ���� ĥ���� ���� �׷��� �ִ�. -> ��ư ���� ��Ʈ.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Hat") // ������
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(96, 101); //������ Ȯ�� ���
                        Debug.Log("��� ��� : ������ ��� ������ �� ���ƺ��� �������̴�. �� ������ �ȵ� �� ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Trash") // ���������
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(102, 109); // ��������� ���

                        Debug.Log("��� ��� : ���� �Ǽ�������� �׿� �ִ�. �˴� ���� �콽�� �Ժη� ������ �ȵ� �� ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "Newspaper") // �Ź���
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(108, 123); // ���Ұ�
                        Debug.Log("��� ��� : �� ���� �� ��¥�� �Ź��� ������ �ִ�. -> �Ǽ��� ���� ���� ���");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "DrumCement") // �巳��+�ø�Ʈ����
                {
                    if (NowState.activeSelf == false)
                    {
                        ScriptStart(124, 126); // ���Ұ�
                        Debug.Log("��� ��� : �巳��� �ø�Ʈ ���밡 ���� �ִ�. ... ū ����� ���� �� ����.");
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
            }
        }
        if (isLastScript)
        {
            if (theDM.isDialogue == false)
            {
                SceneManager.LoadScene("5F_KeyRoom");
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

    void ScriptStart(int Start_num, int End_num) //��� ȣ��
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

    }

    void EndScriptStart(int Start_num, int End_num) // �� �̵� ���� ��� ȣ��
    {
        theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), Start_num, End_num);

        isLastScript = true;
    }
}

