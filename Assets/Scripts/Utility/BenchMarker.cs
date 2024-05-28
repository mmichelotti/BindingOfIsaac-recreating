using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BenchMarker : MonoBehaviour
{
    System.Diagnostics.Stopwatch stopwatch = new();

    private List<Vector2> targetTest = new();
    
    private Vector2 center = new(25, 25);
    private void Start()
    {
        // fill a list with random target Vector2
        for (int i = 0; i < 100000; i++)
        {
            targetTest.Add(new(Random.Range(0, 50), Random.Range(0, 50)));
        }

        //warmup the cpu and so it chaces some informations and the resutl of the benchmark arent influenced
        foreach (var item in targetTest)
        {
            DirectionUtility.GetDirectionTo(center, item);
        }


        //benchmark get offset
        stopwatch.Start();
        foreach (var item in targetTest)
        {
            DirectionUtility.GetOffset(center, item);
        }
        stopwatch.Stop();
        System.TimeSpan timeOfApproachA = stopwatch.Elapsed;

        //benchmark get direction
        stopwatch.Reset();
        stopwatch.Start();
        foreach (var item in targetTest)
        {
            DirectionUtility.GetDirectionTo(center, item);
        }
        stopwatch.Stop();
        System.TimeSpan timeOfApproachB = stopwatch.Elapsed;

        //which is faster?
        if (timeOfApproachA > timeOfApproachB)
        {
            Debug.Log($"get offset is {(timeOfApproachA - timeOfApproachB) / timeOfApproachB} slower.");
        }
        else { 
            Debug.Log($"bitwise is {(timeOfApproachB - timeOfApproachA)/timeOfApproachA} slower");
        
        }
        
    }
}
