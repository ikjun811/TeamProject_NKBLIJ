using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class FailScene : MonoBehaviour
{

    public GameObject NowLocate;

    private void Awake()
    {
        NowLocate = GameObject.Find("NowLocateText");
        NowLocate.SetActive(false);
    }

}
