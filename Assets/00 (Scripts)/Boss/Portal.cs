using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) 
    {
        if(col.gameObject.CompareTag("PLAYER"))
        {
            SceneManager.LoadScene("Boss1");
        }
    }
}
