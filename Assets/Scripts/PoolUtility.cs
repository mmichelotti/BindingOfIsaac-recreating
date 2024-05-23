using System.Collections.Generic;
using UnityEngine;

public static class PoolUtility
{
    public static void PreparePooling<T>(this Queue<T> queue, T prefab, int amount, Transform parent = null) where T : MonoBehaviour
    {
        for (int i = 0; i < amount; i++)
        {
            T instance = Object.Instantiate(prefab, parent);
            queue.Enqueue(instance);
        }
    }

    public static T Cycle<T>(this Queue<T> queue)
    {
        T temp = queue.Dequeue();
        queue.Enqueue(temp);
        return temp;
    }

}