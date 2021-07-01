using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossUi : MonoBehaviour
{
    public GameObject win;
    public GameObject lose;

    void Update()
    {
        if(DataManager.Instance.isDie)
            lose.SetActive(true);
        
        if(DataManager.Instance.isWin)
            win.SetActive(true);
    }

    public void go()
    {
        SceneManager.LoadScene("Camp");
    }
}
