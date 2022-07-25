using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private Player player = null;
    private ItemDataManager itemData;
    private InventoryUI inventoryUI;

    public Player MainPlayer { get => player; }

    public ItemDataManager ItemData
    {
        get => itemData;
    }

    public InventoryUI InvenUI => inventoryUI;

    // static 맴버 변수 : 주소가 고정 => 이 클래스의 모든 인스턴스는 같은 값을 가진다.
    private static GameManager instance = null;
    public static GameManager Inst { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    private void Initialize()
    {
        itemData = GetComponent<ItemDataManager>();

        player = FindObjectOfType<Player>();
        inventoryUI = FindObjectOfType<InventoryUI>();
    }
}
