using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SpawnManager SpawnManager { get; private set; }
    public GridManager GridManager { get; private set; }

    private void Awake()
    {
        Instance = this.MakeSingleton();
        SpawnManager = this.GetManager<SpawnManager>();
        GridManager = this.GetManager<GridManager>();
    }
}
