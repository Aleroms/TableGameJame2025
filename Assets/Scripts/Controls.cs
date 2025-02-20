using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Camera _camera;
    private GameObject currentBlock;
    private BlockSpawner _blockSpawner;
    private bool canSetBlockPosition;

    [SerializeField] private Transform left, right;
    [SerializeField] private float blockHeight;


    void Start()
    {
        _camera = Camera.main;
        _blockSpawner = GameObject.FindWithTag("blockSpawner")
            .GetComponent<BlockSpawner>();

        SpawnBlock();
    }

    private void SpawnBlock()
    {
        currentBlock = _blockSpawner.SpawnBlock();
        canSetBlockPosition = true;
    }
    void Update()
    {
        if (canSetBlockPosition)
        {
            currentBlock.transform.position = SetBlockPosition();
        }

        //once player right clicks the block should fall
        if (Input.GetMouseButtonDown(0))
        {
            canSetBlockPosition = false;
        }


    }

    private Vector3 SetBlockPosition()
    {
        // the vector where the block will follow the user's mouse cursor
        var blockPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        blockPosition.z = 0;
        blockPosition.y = blockHeight;

        // <-->
        var x = blockPosition.x;
        if (x < left.position.x)
        {
            x = left.position.x;
        }
        if (x > right.position.x)
        {
            x = right.position.x;
        }
        blockPosition.x = x;
        return blockPosition;
    }

    // this method is called once the block has triggered a collision with
    // the scale, then it can spawn a new block to use at the top.
    public void BlockHasLanded()
    {
        SpawnBlock();
    }
}
