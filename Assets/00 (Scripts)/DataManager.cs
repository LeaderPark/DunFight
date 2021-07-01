using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public bool isDie = false;
    public bool isWin = false;
    public bool isFade = false;

    [Header("세팅창")]
    // public Slider bgmslider;
    // public Slider effectslider;
    // public AudioSource bgm;
    // public AudioSource effect;
    public GameObject settingPanel;
    public bool isPanelOn = false;
    private bool spawn = true;
    
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<DataManager>();
                if (obj != null)
                {
                    instance = obj;

                }
                else
                {
                    var newSingleton = new GameObject("DataManager").AddComponent<DataManager>();
                    instance = newSingleton;
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<DataManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(!isPanelOn)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                settingPanel.SetActive(true);
                isPanelOn = true;
            }
        }
        else
        {   
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                settingPanel.SetActive(false);
                isPanelOn = false;
            }
        }
    }

    public void SettingOn()
    {
        settingPanel.SetActive(true);
        isPanelOn = true;
    }

    public void SettingOff()
    {
        settingPanel.SetActive(false);
        isPanelOn = false;
    }
}
