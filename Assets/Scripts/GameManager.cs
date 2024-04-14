using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public SpawnManager SpawnManager { get; private set; }
    public GridManager GridManager { get; private set; }

    private void Awake()
    {
        MakeSingleton();
        GetMiniManagers();
    }

    private void MakeSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void GetMiniManagers()
    {
        SpawnManager = GetComponentInChildren<SpawnManager>();
        GridManager = GetComponentInChildren<GridManager>();
    }
}
