using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootObj : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1f * Time.deltaTime);
        transform.Translate(Vector3.right * Time.deltaTime * 3f);
        Destroy(this.gameObject,15f);
    }

    public void Rotate(Vector2 lookPos)
    {
        float lookAngle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
    }

    public void RotateToPlayer(float time)
    {
        StartCoroutine(Timer(() => {
            Vector2 lookPos = player.transform.position - transform.position;
            float lookAngle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
        }, time));
    }

    public IEnumerator Timer(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}
