using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isShoot = false;
    public bool isChase = false;

    private GameObject player;
    int a = 40;
    public GameObject shootObj;
    public GameObject[] fallObj;
    public GameObject[] point;
    Vector3 dir;
    int rotSpeed;

    void Start()
    {
        player = GameObject.Find("Player");
        Debug.Log(player.name);
        StartCoroutine(fallShoot());
        StartCoroutine(patern());
    }

    void Update()
    {
        if(isShoot) 
        {
            StartCoroutine(rotateShoot());
            isShoot = false;
        }
        if(isChase)
        {
            StartCoroutine(chase());
            isChase = false;
        }
        
    }

    IEnumerator rotateShoot()
    {
        for (int i = 0; i < a; i++)
        {
                Instantiate(shootObj, transform.position, Quaternion.Euler(0f, 0f, deviceRotate(a) * i )).GetComponent<ShootObj>();
                yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator chase()
    {
        for (int i = 0; i < 6 ; i++)
        {
            ShootObj obj = Instantiate(shootObj, point[i].transform.position, Quaternion.identity).GetComponent<ShootObj>();;
            obj.RotateToPlayer(0.2f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator fallShoot()
    {
        float rand = UnityEngine.Random.Range(-16, 12);
        int i = UnityEngine.Random.Range(0, 5);
        Instantiate(fallObj[i],new Vector3(rand, 16, 0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        StartCoroutine(fallShoot());
    }

    float deviceRotate(int bulletCount)
    {
        return 1000 / bulletCount;
    }

    IEnumerator patern()
    {
        yield return new WaitForSeconds(10f);
        isShoot = true; 
        yield return new WaitForSeconds(10f);
        isChase = true;
        StartCoroutine(patern());
    }
}
