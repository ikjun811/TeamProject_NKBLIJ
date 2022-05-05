using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Panel : MonoBehaviour
{
    GameObject clikedObj;
    private void Start()
    {
        GameObject clikedObj = null;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                clikedObj = hit.transform.gameObject;
                Debug.Log(clikedObj.name);
            }
        }
        else clikedObj = null;
    }

}
