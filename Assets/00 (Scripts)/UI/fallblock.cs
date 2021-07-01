using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallblock : MonoBehaviour
{
    public GameObject fallSpawn;

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("PLAYER"))
        {
            col.transform.position = new Vector3(fallSpawn.transform.position.x,-1,0);
        }
    }
}
