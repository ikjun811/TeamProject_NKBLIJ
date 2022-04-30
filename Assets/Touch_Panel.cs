using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch_Panel : MonoBehaviour
{
    public Inventory inventory;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
            if(hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                if(click_obj.name == "Book")
                {
                    inventory.AcquireItem(hit.transform.GetComponent<ItemPickUp>().item);
                    Destroy(hit.transform.gameObject);
                }
                else if (click_obj.name == "Lighter")
                {
                    // ��� ���
                    Destroy(click_obj);
                    // ������ ȹ�� UI ���
                    // �κ��丮�� ������ �߰�
                }
                else if (click_obj.name == "Gas")
                {
                    // ��� ���
                    Destroy(click_obj);
                    // ������ ȹ�� UI ���
                    // �κ��丮�� ������ �߰�
                }
                else if (click_obj.name == "DoorLock")
                {
                    Debug.Log(click_obj.name);
                    // ��� ��� (1����)
                    // UI �˾� ��� (��й�ȣ �Է� â)
                }
                else if (click_obj.name == "Window")
                {
                    Debug.Log(click_obj.name);
                    // ��� ���
                }
            }
        }
    }

}