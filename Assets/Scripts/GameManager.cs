using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement; 
public class GameManager : MonoBehaviour
{
    //Made it a singleton 
    public static GameManager _instance; 
    public static GameManager Instance {  get { return _instance; } }

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
    [SerializeField] private int starting_yPos;
    private int previous_weightDiff = 0;
    [SerializeField] private int weightDiff = 0;
    [SerializeField] private int weightDiffThreshold = 100; //Threshold for which game over is triggered 
    [SerializeField] private int weightDiffWarningThreshold = 80; //Threshold for which warning will trigger
    private Transform left_scale;
    private Transform right_scale;
    private float scaleHeightMultiplier = 0.10f; // Used to position y value of scales
    private float new_left_scale_y;
    private float new_right_scale_y;
    private float time_elapsed = 0; // Time spent moving scales
    [SerializeField] private float duration = 50f; //How long it takes for scales to adjust to new position


    //Warning and Game Over 
    private bool warning = false;
    [SerializeField] public int penalty = 0; 
    //Turn system
    [SerializeField] public int turn = 1;
    [SerializeField] private int level = 1;
    [SerializeField] public int merge = 0;
    [SerializeField] private int mergeLevelModulator = 15; //Number of merges that would trigger a level increase
    // Start is called before the first frame update
    void Start()
    {
        blockSpawner.SpawnBlock();
        left_scale = left_detector.gameObject.transform.parent;
        right_scale = right_detector.gameObject.transform.parent;
        new_left_scale_y = starting_yPos;
        new_right_scale_y = starting_yPos;
    }

    // Update is called once per frame
    void Update()
    {
        MoveScale();
        // Get the weightDiff, and if it has changed, set the new scale height destination
        WarningAndGameOver(); 
    }

    void SetNewScaleHeight()
    {
        new_left_scale_y = starting_yPos + (weightDiff * scaleHeightMultiplier);
        new_right_scale_y = starting_yPos - (weightDiff * scaleHeightMultiplier);
    }

    void MoveScale()
    {
        weightDiff = (right_detector.currentWeight - left_detector.currentWeight);
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
        if (Mathf.Abs(weightDiff) >= weightDiffWarningThreshold)
        {
            Warning(true); 
        }
        else
        {
            Warning(false); 
        }
        if ((Mathf.Abs(weightDiff) >= weightDiffThreshold) || penalty >= 3)
        {
            GameOver();
        }
    }

    void Warning(bool isWarning)
    {
        warning = isWarning; 
        if(isWarning)
        {
            //Activate warning state 
            Debug.Log("Warning activated"); 
        }
        else
        {
            //Deactivate warning state 
            Debug.Log("Warning deactivated"); 
        }
    }
    void GameOver()
    {
        SceneManager.LoadSceneAsync("Game Over"); 
    }
    public void ProgressTurn()
    {
        turn++; 
        if(merge%mergeLevelModulator == 0)
        {
            level++;
            Debug.Log("Level increased. Current level: " + level);
        }
    }
}
