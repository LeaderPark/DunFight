using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{ 
    public GameObject fade;
    public Image fadeImg;
    private float waitTime = 0;
    private bool isFade = false;
    
    void Update()
    {
        if(isFade)
            StartCoroutine(fadeCol());
    }
    
    public void StartGame()
    {
        isFade = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SettingOn()
    {
        DataManager.Instance.settingPanel.SetActive(true);
        DataManager.Instance.isPanelOn = true;
    }

    public void SettingOff()
    {
        DataManager.Instance.settingPanel.SetActive(false);
        DataManager.Instance.isPanelOn = false;
    }

    IEnumerator fadeCol()
    {
        fade.SetActive(true);
        if(waitTime <= 1f){
            waitTime += Time.deltaTime * 0.5f;
            fadeImg.color = new Color(0,0,0, waitTime);
        }
        else
        {
            SceneManager.LoadScene("Lobby");
        }  
        yield return null;
    }

}
