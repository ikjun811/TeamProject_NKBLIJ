using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailScene : MonoBehaviour
{

    public void goToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
