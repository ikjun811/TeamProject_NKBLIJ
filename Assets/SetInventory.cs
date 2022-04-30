using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInventory : MonoBehaviour
{
    public GameObject inventory;

    public void SetInventoryOff()
    {
        inventory.SetActive(false);
    }
    public void SetInventoryOn()
    {
        inventory.SetActive(true);
    }
}
