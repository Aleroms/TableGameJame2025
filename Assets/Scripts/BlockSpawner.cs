using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
This script is a manager that instantiates blocks for the user to 
drop. 
 */
public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject currentBlock;
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private Transform _spawnPoint;

    private Queue<GameObject> spawnQueue;

    private void Start()
    {
        
        spawnQueue = new Queue<GameObject>();
        foreach(GameObject bp in blockPrefabs)
        {
            spawnQueue.Enqueue(bp);
        }
    }



    // This method is called and spawns a block at specified spawn point
    public GameObject SpawnBlock()
    {
        // make sure to always have blocks to spawn
        AddBlockToQueue();
        var go = spawnQueue.Dequeue();
        return Instantiate(go, _spawnPoint);
    }
    private void AddBlockToQueue()
    {
        var rand = new System.Random();
        int randIndex = rand.Next(0, blockPrefabs.Length);
        spawnQueue.Enqueue(blockPrefabs[randIndex]);
    }
    
}