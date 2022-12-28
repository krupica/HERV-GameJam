using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    public static Spawner[] _spawners;
    public static Random r;
    public static int torchIndex;
    public static int coinIndex;

    public static void Init()
    {
        _spawners = FindObjectsOfType<Spawner>();
        r = new Random();
    }
    
    private void Move(GameObject go)
    {
        go.transform.position = transform.position;
    }
    
    public static void SpawnCoin(GameObject coin)
    {
        int newIndex = r.Next(0, _spawners.Length);
        while (newIndex == coinIndex || newIndex == coinIndex || newIndex == torchIndex)
        {
            newIndex = r.Next(0, _spawners.Length);
        }

        coinIndex = newIndex;
        _spawners[coinIndex].Move(coin);
    }
    
    public static void SpawnTorch(GameObject torch)
    {
        int newIndex = r.Next(0, _spawners.Length);
        while (newIndex==coinIndex || newIndex==coinIndex || newIndex == torchIndex)
        {
            newIndex = r.Next(0, _spawners.Length);
        }
        
        torchIndex = newIndex;
        _spawners[torchIndex].Move(torch);
    }
}
