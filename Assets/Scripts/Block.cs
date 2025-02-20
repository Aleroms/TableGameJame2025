using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Block : MonoBehaviour
{
    //DONT CHANGE BLOCKTYPE DURING RUNTIME
    // keyword static prevents the type from showing in the inspector
    private enum BlockType { RIGHTTRI, EQTRI, RECT, BRIDGE }
    private enum BlockWeightLevel { Level1, Level2, Level3, Level4 }
    [SerializeField] private BlockType type;
    [SerializeField] private BlockWeightLevel weight;


    [SerializeField] private int BlockWeight; 


    [SerializeField] private bool CanCombine = true; 

    // defines the block combination progression
    [SerializeField] private Sprite[] BlockSprites;
    [SerializeField] private Sprite CurrentSprite;
    private SpriteRenderer sp; 
    private void Start()
    {
        //4 prefabs one for each shape
        //weight and sprite need to be updated when they combine/on runtime 
        float randomSeed = Random.Range(0, 1); 
        if(randomSeed >= 0.5)
        {
            weight = BlockWeightLevel.Level1; 
        }
        else
        {
            weight = BlockWeightLevel.Level2;
        }
        if (sp = GetComponent<SpriteRenderer>())
        {
            ChangeSprite();
        }
        // i am not sure what this means ^
        // maybe you can help me understand

    }

    private void ChangeSprite()
    {
        // a better way is to create a hash and map the value
        var hashmap = new Dictionary<BlockWeightLevel, Sprite>();
        hashmap.Add(BlockWeightLevel.Level1, BlockSprites[0]);
        hashmap.Add(BlockWeightLevel.Level2, BlockSprites[1]);
        hashmap.Add(BlockWeightLevel.Level3, BlockSprites[2]);
        hashmap.Add(BlockWeightLevel.Level4 , BlockSprites[3]);

        //Change CurrentSprite component, and update renderer to match 
        CurrentSprite = hashmap[weight];
        
        sp.sprite = CurrentSprite;
    }

    private void ChangeWeight()
    {
        //Changes individual weight based on current weight level value & sprite shape. 
        if (type == BlockType.RIGHTTRI || type == BlockType.EQTRI)
        {
            switch (weight)
            {
                case BlockWeightLevel.Level1:
                    BlockWeight = 5; 
                    break;
                case BlockWeightLevel.Level2:
                    BlockWeight = 10; 
                    break;
                case BlockWeightLevel.Level3:
                    BlockWeight = 15; 
                    break;
                case BlockWeightLevel.Level4:
                    BlockWeight = 20; 
                    break;
            }
        }
        else
        {
            switch (weight)
            {
                case BlockWeightLevel.Level1:
                    BlockWeight = 10;
                    break;
                case BlockWeightLevel.Level2:
                    BlockWeight = 20;
                    break;
                case BlockWeightLevel.Level3:
                    BlockWeight = 30;
                    break;
                case BlockWeightLevel.Level4:
                    BlockWeight = 40;
                    break;
            }
        }
    }


}
