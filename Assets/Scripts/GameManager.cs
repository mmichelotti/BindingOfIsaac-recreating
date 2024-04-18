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
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void GetMiniManagers()
    {
        SpawnManager = GetComponentInChildren<SpawnManager>();
        GridManager = GetComponentInChildren<GridManager>();
    }
}
