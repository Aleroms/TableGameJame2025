using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to detect how many blocks are on top of a scale and sum the weights

public class BlockDetector : MonoBehaviour
{
    public int currentWeight;
    // it would be nice to have a UI display the currentweight

    // Start is called before the first frame update
    void Start()
    {
        currentWeight = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Block block = collision.GetComponent<Block>();
        block.detector = this;
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
    // this is used when blocks combine
    public void UpdateWeight(int oldWeight, int newWeight)
    {
        currentWeight -= oldWeight;
        currentWeight += newWeight;
    }
}
