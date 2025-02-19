using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Block : MonoBehaviour
{
    //DONT CHANGE BLOCKTYPE DURING RUNTIME
    private enum BlockType { RIGHTTRI, EQTRI, RECT, BRIDGE }
    [SerializeField] private static BlockType type;
    private enum BlockWeightLevel { Level1, Level2, Level3, Level4 }
    [SerializeField] private BlockWeightLevel weight;
    [SerializeField] private int BlockWeight; 
    [SerializeField] private bool CanCombine = true; 
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
        if(sp = GetComponent<SpriteRenderer>())
        {
            ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        //Change CurrentSprite component, and update renderer to match 
        if(weight == BlockWeightLevel.Level1)
        {
            CurrentSprite = BlockSprites[0]; 
        }
        else if(weight == BlockWeightLevel.Level2)
        {
            CurrentSprite = BlockSprites[1];
        }
        else if(weight == BlockWeightLevel.Level3)
        {
            CurrentSprite = BlockSprites[2];
        }
        else
        {
            CurrentSprite = BlockSprites[3];
        }
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
