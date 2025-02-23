using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement; 
public class GameManager : MonoBehaviour
{
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
    [SerializeField] private Transform left_scale_left_boundary;
    [SerializeField] private Transform left_scale_right_boundary;
    [SerializeField] private Transform right_scale_left_boundary;
    [SerializeField] private Transform right_scale_right_boundary;
    private float scaleHeightMultiplier = 0.10f; // Used to position y value of scales
    private float new_left_scale_y;
    private float new_right_scale_y;
    private float time_elapsed = 0; // Time spent moving scales
    [SerializeField] private float duration = 10f; //How long it takes for scales to adjust to new position

    //Turn system
    [SerializeField] public int turn = 1;
    [SerializeField] private MousePosition mousePosition;
    [SerializeField] private UIManager uiManager; 
    [SerializeField] private GameObject GameOverPanel; 
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
        // Get the weightDiff, and if it has changed, set the new scale height destination
        //Debug.Log("Left Scale: " + left_detector.currentWeight);
        //Debug.Log("Right Scale: " + right_detector.currentWeight);
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
            left_scale.position = new Vector2(left_scale.position.x, Mathf.Lerp(left_scale.position.y, new_left_scale_y, time_elapsed/duration));
            right_scale.position = new Vector2(right_scale.position.x, Mathf.Lerp(right_scale.position.y, new_right_scale_y, time_elapsed / duration));
        }

        if(Mathf.Abs(weightDiff) >= weightDiffWarningThreshold)
        {
            //Which one is the heavier scale?
            if(right_detector.currentWeight < left_detector.currentWeight)
            {
                uiManager.TintLeft();  
            }
            else
            {
                uiManager.TintRight(); 
            }
        }
        else
        {
            if (right_detector.currentWeight < left_detector.currentWeight)
            {
                uiManager.RestoreLeft();
            }
            else
            {
                uiManager.RestoreRight();
            }
        }
        if(Mathf.Abs(weightDiff) >= weightDiffThreshold)
        {
            GameOver(); 
        }
    }

    void SetNewScaleHeight()
    {
        //if (weightDiff >= 0)
        //{
        //    new_left_scale_y = starting_yPos - weightDiff;
        //    new_right_scale_y = starting_yPos + weightDiff;
        //}
        //else
        //{
        //    new_left_scale_y = starting_yPos + weightDiff;
        //    new_right_scale_y = starting_yPos - weightDiff;
        //}
        new_left_scale_y = starting_yPos + (weightDiff * scaleHeightMultiplier);
        new_right_scale_y = starting_yPos - (weightDiff * scaleHeightMultiplier);
        //Debug.Log("---Left: " + new_left_scale_y);
        //Debug.Log("---Right: " + new_right_scale_y);
    }

    void GameOver()
    {
        FindObjectOfType<AudioManager>().turns = turn; 
        SceneManager.LoadSceneAsync("Game Over"); 
    }
    public void ProgressTurn()
    {
        turn++; 
        if(turn%2 == 0) //Even turns is right boundary, odd turns is left boundary 
        {
            mousePosition.boundaryLeft = right_scale_left_boundary; 
            mousePosition.boundaryRight = right_scale_right_boundary;
            uiManager.HighlightRight();
            uiManager.UpdateTurn(turn); 
        }
        else
        {
            mousePosition.boundaryLeft = left_scale_left_boundary; 
            mousePosition.boundaryRight = left_scale_right_boundary;
            uiManager.HighlightLeft();
            uiManager.UpdateTurn(turn);
        }
    }
}
