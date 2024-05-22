using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;


    // per ALE: 
    // questo getter mi riesegue la fnzione GetComponentInChildren ogni volta che SpawnManager viene chiamato
    // quindi è megio fare come nel metodo GetMiniManagers(), ovvero che setto un riferimento una volta sola all'inizio
    // e ogni volta che viene chiamato SpawnManager non deve rievocare il metodo?
    public SpawnManager SpawnManager => GetComponentInChildren<SpawnManager>();
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
        //SpawnManager = GetComponentInChildren<SpawnManager>();
        GridManager = GetComponentInChildren<GridManager>();
    }
}
