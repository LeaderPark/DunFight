using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    Transform tr;

    public float limitMinX;
    public float limitMaxX;
    public float limitMinY;
    public float limitMaxY;

    private void Start() {
        tr = player.transform;
        player = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        float x = Mathf.Clamp(tr.position.x, limitMinX, limitMaxX);
        float y = Mathf.Clamp(tr.position.y, limitMinY, limitMaxY);
        transform.position = new Vector3(x,y, -10);
    }
}
