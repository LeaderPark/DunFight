using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public Image gameoverPanel;
    public GameObject gameoverPanelObj;
    private float waitTime;

    void Update()
    {
        if(DataManager.Instance.isDie)
        {
            StartCoroutine(isDie());
        }
    }

    public void goLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    IEnumerator isDie()
    {
        yield return new WaitForSeconds(1f);

        if(waitTime <= 0.8f){
            waitTime += Time.deltaTime;
            gameoverPanel.color = new Color(0,0,0, waitTime);
        }
        else {
            gameoverPanelObj.SetActive(true);
        }   
    }
}
