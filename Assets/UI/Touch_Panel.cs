using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Touch_Panel : MonoBehaviour
{
    UIManager um;
    private GameObject clikedObj;
    public Inventory inventory;
    public Text NewItemAddText;

    private bool flag_Lighter;
    private bool flag_Gas;
    public GameObject DoorLockPanel;
    public GameObject DoorLock;

    DialogueManager theDM;

    InteractionEvent getdialog;


    private void Start()
    {
        clikedObj = null;
        um = GameObject.Find("UIManager").GetComponent<UIManager>();

        flag_Lighter = false;
        flag_Gas = false;

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
                Debug.Log(clikedObj.name);
                if (clikedObj.name == "Lighter")
                {
                    // 대사 출력
                    theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), 36, 41);
                    NewItemAddText.GetComponent<Text>().text = "아이템 획득 : 라이터";
                    inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                    Destroy(clikedObj);
                    um.NewItemAddPanelOn();
                    flag_Lighter = true;
                }
                else if (clikedObj.name == "Gas")
                {
                    // 대사 출력
                    NewItemAddText.GetComponent<Text>().text = "아이템 획득 : 라이터 기름";
                    inventory.AddItem(clikedObj.GetComponent<Item_PickUp>().item);
                    Destroy(clikedObj);
                    um.NewItemAddPanelOn();
                    flag_Gas = true;
                }
                else if (clikedObj.name == "DoorLock")
                {
                    // 1. 대사 출력
                    if (flag_Gas == true && flag_Lighter == true) // 분해 구현 시, Box & Gas 구분 + 조합된 아이템 구분
                    {
                        DoorLockPanelOn();
                    }
                }
                else if (clikedObj.name == "Book")
                {
                    // 대사 출력
                    theDM.ShowDialogue(getdialog.GetComponent<InteractionEvent>().GetDialogue(), 1, 18);
                }
                else if (clikedObj.name == "Window")
                {
                    // 대사 출력
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
}
