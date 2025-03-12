using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    //Made it a singleton 
    public static NewGameManager _instance;
    public static NewGameManager Instance { get { return _instance; } }

    public Collider2D minHeightContainer;

    [SerializeField] private Shaker shaker; 
    [SerializeField] private BlockSpawner blockSpawner;

    [SerializeField] private int turnsTillQuake = 10;

    [SerializeField] private int quakeExtra = 5; 

    public int currentTurns = 0; 

    public bool mergeActive = false; 
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        blockSpawner.SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MinHeight()
    {

    }

    public void ProgressTurn()
    {
        currentTurns++;
        if (currentTurns >= turnsTillQuake)
        {
            currentTurns = 0;
            shaker.Shake(); 
            turnsTillQuake += quakeExtra; 
        }
    }
}
