using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;


    // per ALE: 
    // questo getter mi riesegue la fnzione GetComponentInChildren ogni volta che SpawnManager viene chiamato
