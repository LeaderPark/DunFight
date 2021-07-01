using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public GameObject fade;
    public Image fadeImg;
    private float waitTime = 1f;
    private bool isFade = false;

    void Start()
    {
        fadeImg.color = new Color(0,0,0, 255);
        isFade = true;
    }

    void Update()
    {
        if(isFade)
            StartCoroutine(fadeCol());
        if(DataManager.Instance.isFade)
            StartCoroutine(fadeoutCol());
    }

    IEnumerator fadeCol()
    {
        yield return null;
        if(waitTime > 0f){
            waitTime -= Time.deltaTime * 0.3f;
            fadeImg.color = new Color(0,0,0, waitTime);
        }
        else
        {
            fade.SetActive(false);
        }    
    }

    IEnumerator fadeoutCol()
    {
        fade.SetActive(true);
        if(waitTime <= 1f){
            waitTime += Time.deltaTime * 0.5f;
            fadeImg.color = new Color(0,0,0, waitTime);
        }
        else
        {
            SceneManager.LoadScene("Camp");
        }  
        yield return null;
    }

    public void skip()
    {
        DataManager.Instance.isFade = true;
    }
}
