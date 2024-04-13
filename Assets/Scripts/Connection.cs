using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    //si lo so che � praticamente una classe game object hahaha mi ha fatto ridere
    //� solo che ha un costruttore diverso e mi faceva comodo per tenere il codice ordinato
    //ok pi� passa il tempo pi� mi fa ridere non la canceller�
    public GameObject GameObject { get; private set; }
    public Connection(GameObject prefab, Transform parent)
    {
        GameObject = Object.Instantiate(prefab, parent);
    }
    public void Initialize(string name, Vector3 position, Quaternion rotation)
    {

        GameObject.name = name;
        GameObject.transform.SetPositionAndRotation(position, rotation);
    }
    public void SetActive(bool isActive)
    {
        GameObject.SetActive(isActive);
    }
}
