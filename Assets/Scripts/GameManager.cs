using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //Made it a singleton 
    public static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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
    [SerializeField] private BlockSpawner blockSpawner;
    //Turn system
    [SerializeField] public int turn = 1;
    [SerializeField] public int level = 1;
    [SerializeField] public int merge = 0;
    [SerializeField] private int mergeLevelModulator = 10; //Number of merges that would trigger a level increase
    [SerializeField] public int score = 0;
    [SerializeField] public int consecutive = 0;

    [SerializeField] private float gravityScale = -20.0f; 
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, gravityScale, 0);
        blockSpawner.SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void WarningAndGameOver()
    {
        
        
    }
    void GameOver()
    {
        SceneManager.LoadSceneAsync("Game Over");
    }
    public void ProgressTurn()
    {
        consecutive = 0; 
        turn++;
        if (merge >= mergeLevelModulator)
        {
            level++;
            merge = 0;
            mergeLevelModulator += 5;
            Debug.Log("Level increased. Current level: " + level);
        }
    }


    public int UpdateScore(int blockTier)
    {
        int dScore = (10 + blockTier - 1) * (level + 1);  // base score
        dScore = Mathf.RoundToInt(dScore * Mathf.Pow(1.5f, Mathf.Min(consecutive, 5)));  // combo multiplier: 1x, 1.5x, 2.3x, 3.4x, 5.1x, 7.6x
        score += dScore; 
        return dScore; 
    }
}
