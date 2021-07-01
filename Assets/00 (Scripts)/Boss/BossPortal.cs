using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPortal : MonoBehaviour
{
    public GameObject keyCode;
    
    void OnTriggerEnter2D(Collider2D col) 
    {
        keyCode.SetActive(true);
        
        if(col.gameObject.CompareTag("PLAYER"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene("Boss1");
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        keyCode.SetActive(false);
    }
}
