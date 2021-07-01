using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CampUI : MonoBehaviour
{
    public GameObject fade;
    public Image fadeImg;
    private float waitTime = 1f;
    private bool isFade = false;
    public GameObject F;

    void Start()
    {
        fadeImg.color = new Color(0,0,0, 255);
        isFade = true;
        DataManager.Instance.isFade = false;
        DataManager.Instance.isDie = false;
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
            waitTime += Time.deltaTime * 0.8f;
            fadeImg.color = new Color(0,0,0, waitTime);
        }
        else
        {
            SceneManager.LoadScene("Stage1");
        }  
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        F.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D col) {
        F.SetActive(false);
    }
}
