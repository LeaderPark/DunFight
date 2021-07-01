using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public Grid grid;
    public List<GameObject> rooms;

    public GameObject boss;
    public GameObject panel;

    private float waitTime = 1;
    private float spawntime = 1;
    private bool spawnedBoss;

    void Update()
    {
        if(spawntime <= 0)
        {
            if(rooms.Count < 4 || rooms.Count > 6)
            {
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentScene);
            }
            else
            {
                panel.SetActive(false);
            }
        }
        else if(spawntime >= 0)
        {
            spawntime -= Time.deltaTime;
        }

        if(waitTime <= 0 && spawnedBoss == false)
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                if(i == rooms.Count-1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else if(waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
    }
}
