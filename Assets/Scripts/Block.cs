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

    // BlockDetector.cs updates this when the block enters its trigger collider
    public BlockDetector detector;

    // Block audio during collisions
    private AudioSource audioSource;
    private Collision2D lastHitObject = null;

    private void Start()
    {
        spriteByWeight = new Dictionary<BlockWeightLevel, Sprite>();
        sp = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        InitializeBlockWeight();
        InitializeSpriteWeightDict();
        ChangeWeight();


    }

    private void InitializeBlockWeight()
    {
        weight = BlockWeightLevel.Level1;
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
                    BlockWeight = 20; 
                    break;
                case BlockWeightLevel.Level4:
                    BlockWeight = 40; 
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
                    BlockWeight = 40;
                    break;
                case BlockWeightLevel.Level4:
                    BlockWeight = 80;
                    break;
            }
        }
        ChangeSprite();
        if (detector != null)
        {
            detector.UpdateWeight(BlockWeight / 2, BlockWeight);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other != lastHitObject)
        {
            lastHitObject = other;
            audioSource.Play();
            if (!audioSource.mute)
            {
                StartCoroutine(AudioTimer());
            }
        }
        
        // Hitting another block
        var otherBlockScript = other.gameObject.GetComponent<Block>();
        if (otherBlockScript != null && CanCombine)
        {
            otherBlockScript.CanCombine = false;
            var otherWeight = otherBlockScript.weight;
            var otherType = otherBlockScript.type;

            // combine if other is same block type and weight
            if (type == otherType && weight == otherWeight)
            {
                CombineBlocks();
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator AudioTimer()
    {
        // Wait before playing a sound again, to avoid repeat sounds
        yield return new WaitForSeconds(0.25f);
        audioSource.mute = true;
        yield return new WaitForSeconds(2);
        audioSource.mute = false;
    }
}
