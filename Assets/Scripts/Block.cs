using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Block : MonoBehaviour
{
    //DONT CHANGE BLOCKTYPE DURING RUNTIME
    private enum BlockType { EQ, TRI, RECT, BRIDGE, MOON }
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
        ChangeScale(); 

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
        GameManager.Instance.merge++;
        GameManager.Instance.consecutive++; 
        if(weight == BlockWeightLevel.Level2)
        {
            GameManager.Instance.UpdateScore(1); 
        }
        else if(weight == BlockWeightLevel.Level3)
        {
            GameManager.Instance.UpdateScore(2); 
        }
        else if(weight == BlockWeightLevel.Level4)
        {
            GameManager.Instance.UpdateScore(3);
            Destroy(this.gameObject); 
        }
        ChangeSprite();
        ChangeWeight();
        ChangeScale(); 

    }

    private void ChangeScale()
    {
        switch(weight)
        {
            case BlockWeightLevel.Level1: 
                break; 
            case BlockWeightLevel.Level2:
                this.gameObject.transform.localScale *= 1.2f; 
                break; 
            case BlockWeightLevel.Level3:
                this.gameObject.transform.localScale *= 1.2f; 
                break; 
            case BlockWeightLevel.Level4: 
                break;
        }
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
        if (type == BlockType.TRI)
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
        else if(type == BlockType.BRIDGE)
        {
            switch (weight)
            {
                case BlockWeightLevel.Level1:
                    BlockWeight = 6;
                    break;
                case BlockWeightLevel.Level2:
                    BlockWeight = 12;
                    break;
                case BlockWeightLevel.Level3:
                    BlockWeight = 24;
                    break;
                case BlockWeightLevel.Level4:
                    BlockWeight = 48;
                    break;
            }
        }
        else if(type == BlockType.RECT)
        {
            switch (weight)
            {
                case BlockWeightLevel.Level1:
                    BlockWeight = 7;
                    break;
                case BlockWeightLevel.Level2:
                    BlockWeight = 14;
                    break;
                case BlockWeightLevel.Level3:
                    BlockWeight = 28;
                    break;
                case BlockWeightLevel.Level4:
                    BlockWeight = 56;
                    break;
            }
        }
        else if(type == BlockType.EQ)
        {
            switch (weight)
            {
                case BlockWeightLevel.Level1:
                    BlockWeight = 4;
                    break;
                case BlockWeightLevel.Level2:
                    BlockWeight = 8;
                    break;
                case BlockWeightLevel.Level3:
                    BlockWeight = 16;
                    break;
                case BlockWeightLevel.Level4:
                    BlockWeight = 32;
                    break;
            }
        }
        else if(type == BlockType.MOON)
        {
            switch (weight)
            {
                case BlockWeightLevel.Level1:
                    BlockWeight = 3;
                    break;
                case BlockWeightLevel.Level2:
                    BlockWeight = 6;
                    break;
                case BlockWeightLevel.Level3:
                    BlockWeight = 12;
                    break;
                case BlockWeightLevel.Level4:
                    BlockWeight = 24;
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
