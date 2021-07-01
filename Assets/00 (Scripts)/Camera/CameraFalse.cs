using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFalse : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("player(Clone)");
    }
    
    void Update()
    {
        this.transform.position = player.transform.position;
    }

}
