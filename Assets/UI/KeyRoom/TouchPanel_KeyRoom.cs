using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchPanel_KeyRoom : MonoBehaviour
{
    public UIManager um;
    public Inventory inventory;
    public GameObject ip; // itemPanel �� ���� ��ũ��Ʈ ������
    public ItemPanel itempanel;
    public GameObject NowState; // ����� Text
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
        NowLocate.GetComponent<Text>().text = "���� ��ġ : ������ ��";
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
                if (clikedObj.name == "Key") // ��ư�� �� ����
                {
                    if (NowState.activeSelf == false)
                    {  // ��� �� ����, 1ȸ ���
                        if (FloorFlag == true)
                        {
                            EndScriptStart(3, 16); //���� ȹ�� & ���� ��ȯ
                            Debug.Log("��� ��� : ���� �ٴ��� ���� ���� �پ� �������� Ű�� �����ϴ� ���� ���");
                            inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                            Destroy(clikedObj);
                            //um.NewItemAddPanelOn("������ ȹ�� : " + clikedObj.name);
                            Debug.Log("��� ��� : �Ƹ� �̰��� �� ���� ����ϴ� ������ ���̴�... ������ ���ư���...");
                            //SceneManager.LoadScene("5F_Corridor");
                        }
                        else if (keyTouchFlag == false)
                        {
                            ScriptStart(17, 26); //���� 1�� ��ȣ�ۿ�
                            Debug.Log("��� ��� : ���ʿ� Ű�� ���δ�. ��� �ƹ����� ���� �����ѵ�...");
                            keyTouchFlag = true;
                        }
                        else if (keyTouchFlag == true)
                        {
                            isGameover = true;
                            EndScriptStart(27, 32); //����2Ʈ ���ӿ���
                            Debug.Log("��� ��� : ���� ����� ����, �ٴ��� ��������� �Ʒ��� ��ι��� ġ�� �����ߴ�....");
                            //SceneManager.LoadScene("Fail");
                        }
                    }
                    else // ������ ��� ���� ���� 
                    {
                        ScriptStart(1, 1); // ���Ұ�
                    }
                    NowStateMsgCheck();
                }
                else if (clikedObj.name == "IronBall") // ��ö�� ����
                {
                    if (NowState.activeSelf == false) // �� ��� �� �ƴ� ��.
                    {
                        ScriptStart(33, 39); //��ö�� ȹ��
                        Debug.Log("��� ��� : �ٴڿ� ��ö���� ���� �ִ�. �ƹ� �ǹ̾��� ���� ���� �� ���� ������...");
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
                else if (clikedObj.name == "Floor") // �ٴ� ����
                {
                    if (NowState.activeSelf == false && FloorFlag == false) // �� ��� �� �ƴ� ��.
                    {
                        ScriptStart(40, 49); //�ٴ� Ȯ��
                        Debug.Log("��� ��� : �ٴ��� �ε�����Ҵ�. �� �� �Ҹ�? -> �����ΰ�?");
                    }
                    else if (NowState.activeSelf == false && FloorFlag == true)
                    {
                        ScriptStart(61, 63); //�μ��� �ٴ� Ȯ��
                        Debug.Log("��� ��� : �ٴ� ����� -> � �ٴ�");
                    }
                    else
                    {
                        string tempItemName = itempanel.getItem();
                        if (tempItemName == "IronBall")
                        {
                            ScriptStart(50, 60); //
                            Debug.Log("��� ��� : �ٴڿ� ��ö���� ������ -> �ٴ��� ������.");
                            floor.GetComponent<SpriteRenderer>().enabled = true;
                            inventory.RemoveItem("IronBall");
                            FloorFlag = true;
                        }
                        else
                        {
                            ScriptStart(1, 1); //���Ұ�
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

