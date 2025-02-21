using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Block : MonoBehaviour
{
    //DONT CHANGE BLOCKTYPE DURING RUNTIME
    private enum BlockType { RIGHTTRI, EQTRI, RECT, BRIDGE }
    private enum BlockWeightLevel { Level1, Level2, Level3, Level4 }

    // this block's type
    [SerializeField] private BlockType type;
    [SerializeField] private BlockWeightLevel weight;
    public int BlockWeight { get; set; }
    private Dictionary<BlockWeightLevel, Sprite> spriteByWeight;


    [SerializeField] private bool CanCombine = true; 

    // defines the block combination progression
    [SerializeField] private Sprite[] BlockSprites;
    private SpriteRenderer sp;


    private void Start()
    {
        spriteByWeight = new Dictionary<BlockWeightLevel, Sprite>();
        sp = GetComponent<SpriteRenderer>();
        InitializeBlockWeight();
        InitializeSpriteWeightDict();
        ChangeWeight();


    }

    private void InitializeBlockWeight()
    {
        float randomSeed = Random.Range(0, 1);
        weight = randomSeed >= 0.5f ? BlockWeightLevel.Level1 : BlockWeightLevel.Level2;
    }
    private void InitializeSpriteWeightDict()
    {
        spriteByWeight.Add(BlockWeightLevel.Level1, BlockSprites[0]);
        spriteByWeight.Add(BlockWeightLevel.Level2, BlockSprites[1]);
        spriteByWeight.Add(BlockWeightLevel.Level3, BlockSprites[2]);
        spriteByWeight.Add(BlockWeightLevel.Level4, BlockSprites[3]);
    }
    private void CombineBlocks()
    {
        weight++;
        ChangeSprite();
        ChangeWeight();
    }

    private void ChangeSprite()
    {
        //makes it so no index outofbounds
        if(weight <= BlockWeightLevel.Level4)
        {
            sp.sprite = spriteByWeight[weight];
        }
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

    private void OnCollisionEnter2D(Collision2D other)
    {

        var otherBlockScript = other.gameObject.GetComponent<Block>();

        if (otherBlockScript != null)
        {
            otherBlockScript.CanCombine = false;
            var otherWeight = otherBlockScript.weight;
            var otherType = otherBlockScript.type;
            Debug.Log(otherType);

            // combine if other is same block type and weight
            if (type == otherType && weight == otherWeight && CanCombine)
            {
                Debug.Log("combining");
                CombineBlocks();
                Destroy(other.gameObject);
            }

            StartCoroutine(UselessAssFunction());

        }



    }
    // need a time delay before setting this canCombine to true because
    // the other script is setting it to false.
    private  IEnumerator UselessAssFunction()
    {
        yield return new WaitForSeconds(0.5f);
        CanCombine = true;
    }

}
