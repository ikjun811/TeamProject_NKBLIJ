using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch_Panel : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.position.x >= 30 && eventData.position.x <= 50 && 
            eventData.position.y >= 150 && eventData.position.y <= 170)
        {
            Debug.Log("Box"); // ������ ���� - �ڽ� -> ���� ��ü�ؼ� �⸧ ����
        }
        else if (eventData.position.x >= 80 && eventData.position.x <= 115 &&
            eventData.position.y >= 230 && eventData.position.y <= 260)
        {
            Debug.Log("Book"); // ��ȭ ��ũ��Ʈ - ���汸�� Hint
        }
        else if (eventData.position.x >= 170 && eventData.position.x <= 180 &&
            eventData.position.y >= 252 && eventData.position.y <= 262)
        {
            Debug.Log("Lighter"); // ������ ���� - ������
        }
        else if (eventData.position.x >= 140 && eventData.position.x <= 150 &&
            eventData.position.y >= 315 && eventData.position.y <= 330)
        {
            Debug.Log("DoorLock"); // ��ȭ ��ũ��Ʈ - ����� + ��й�ȣ �Է�â
        }
        else Debug.Log(eventData.position);
    }

}