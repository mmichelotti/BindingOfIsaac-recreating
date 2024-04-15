using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    public SpawnManager SpawnManager { get; private set; }
    public GridManager GridManager { get; private set; }

    private void Awake()
    {
        MakeSingleton();
        GetMiniManagers();
    }

    private void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void GetMiniManagers()
    {
        SpawnManager = GetComponentInChildren<SpawnManager>();
        GridManager = GetComponentInChildren<GridManager>();
    }
}
