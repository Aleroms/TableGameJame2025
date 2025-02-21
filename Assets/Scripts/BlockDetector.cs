using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

// Used to detect how many blocks are on top of a scale and sum the weights

public class BlockDetector : MonoBehaviour
{
    public int currentWeight;

    // Start is called before the first frame update
    void Start()
    {
        currentWeight = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Block block = collision.GetComponent<Block>();
        if (block != null)
        {
            currentWeight += block.BlockWeight;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Block block = collision.GetComponent<Block>();
        if (block != null)
        {
            currentWeight -= block.BlockWeight;
        }
    }
}
