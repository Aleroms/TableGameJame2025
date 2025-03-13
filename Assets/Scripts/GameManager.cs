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
    [SerializeField] private BlockDetector left_detector;
    [SerializeField] private BlockDetector right_detector;
    [SerializeField] private MousePosition mousePos; 
    [SerializeField] private int starting_yPos;
    private int previous_weightDiff = 0;
    [SerializeField] public int weightDiff = 0;
    public bool leftOrRight = false; //false is left is heavier; true is right is heavier
    [SerializeField] public int weightDiffThreshold = 40; //Threshold for which game over is triggered 
    [SerializeField] public int weightDiffWarningThreshold = 20; //Threshold for which warning will trigger
    private Transform left_scale;
    private Transform right_scale;
    [SerializeField] private float scaleHeightMultiplier = 0.10f; // Used to position y value of scales
    private float new_left_scale_y;
    private float new_right_scale_y;
    private float time_elapsed = 0; // Time spent moving scales
    [SerializeField] private float duration = 50f; //How long it takes for scales to adjust to new position
    //Warning and Game Over 
    public bool warning = false;
    private int gracePeriodTurns = 1;
    public bool gracePeriod = true; 
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
        left_scale = left_detector.gameObject.transform.parent;
        right_scale = right_detector.gameObject.transform.parent;
        new_left_scale_y = starting_yPos;
        new_right_scale_y = starting_yPos;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveScale();
        // Get the weightDiff, and if it has changed, set the new scale height destination
        //weightDiff = left_detector.currentWeight; 
        //WarningAndGameOver();

    }

    void SetNewScaleHeight()
    {
        new_left_scale_y = starting_yPos - (weightDiff * scaleHeightMultiplier);
        new_right_scale_y = starting_yPos + (weightDiff * scaleHeightMultiplier);
    }

    void MoveScale()
    {
        if(right_detector.currentWeight > left_detector.currentWeight)
        {
            leftOrRight = true; 
        }
        else
        {
            leftOrRight = false; 
        }
        weightDiff = Mathf.Abs(right_detector.currentWeight - left_detector.currentWeight);
        if (weightDiff != previous_weightDiff)
        {
            previous_weightDiff = weightDiff;
            SetNewScaleHeight();
            time_elapsed = 0;
        }

        if (left_scale.position.y != new_left_scale_y || right_scale.position.y != new_right_scale_y)
        {
            time_elapsed += Time.deltaTime;
            left_scale.position = new Vector2(left_scale.position.x, Mathf.Lerp(left_scale.position.y, new_left_scale_y, time_elapsed / duration));
            right_scale.position = new Vector2(right_scale.position.x, Mathf.Lerp(right_scale.position.y, new_right_scale_y, time_elapsed / duration));
        }
    }

    void WarningAndGameOver()
    {
        if (left_detector.currentWeight >= weightDiffWarningThreshold)
        {
            warning = true;
            if (left_detector.currentWeight >= weightDiffThreshold)
            {
                gracePeriod = true; 
                if(gracePeriodTurns > 1)
                {
                    GameOver(); 
                }
            }
        }
        else
        {
            warning = false;
            gracePeriod = false; 
        }
        
    }
    void GameOver()
    {
        SceneManager.LoadSceneAsync("Game Over");
    }
    public void ProgressTurn()
    {
        consecutive = 0; 
        turn++;
        if(gracePeriod)
        {
            gracePeriodTurns++; 
        }
        if (merge >= mergeLevelModulator)
        {
            level++;
            merge = 0;
            mergeLevelModulator += 5;
            weightDiffThreshold += 10;
            weightDiffWarningThreshold += 10; 
            Debug.Log("Level increased. Current level: " + level);
        }
    }


    public void UpdateScore(int blockTier)
    {
        switch (blockTier)
        {
            case 1:
                {
                    score += 10 * (level + 1);
                    break;
                }
            case 2:
                {
                    score += 15 * (level + 1);
                    break;
                }
            case 3:
                {
                    score += 20 * (level + 1);
                    break;
                }
            case 4:
                {
                    score += 25 * (level + 1);
                    break;
                }
            case 5:
                {
                    score += 30 * (level + 1);
                    break;
                }
        }
        if(warning)
        {
            score += Mathf.RoundToInt(score * 1.3f); 
        }
        if(consecutive >= 2 && consecutive <= 5)
        {
            switch(consecutive)
            {
                case 2:
                    score += Mathf.RoundToInt(score * 1.1f); 
                    break; 
                case 3:
                    score += Mathf.RoundToInt(score * 1.2f); 
                    break;
                case 4:
                    score += Mathf.RoundToInt(score * 1.3f); 
                    break;
                case 5:
                    score += Mathf.RoundToInt(score * 1.4f); 
                    break; 
            }
        }

    }
}
