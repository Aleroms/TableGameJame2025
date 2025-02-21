using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BlockSpawner blockSpawner;
    [SerializeField] private BlockDetector left_detector;
    [SerializeField] private BlockDetector right_detector;
    [SerializeField] private int starting_yPos;
    private int previous_weightDiff = 0;
    private int weightDiff = 0;
    private Transform left_scale;
    private Transform right_scale;
    private float new_left_scale_y;
    private float new_right_scale_y;
    private float time_elapsed = 0; // Time spent moving scales
    private float duration = 10f; //How long it takes for scales to adjust to new position

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
        Debug.Log("Left Scale: " + left_detector.currentWeight);
        Debug.Log("Right Scale: " + right_detector.currentWeight);
        weightDiff = (right_detector.currentWeight - left_detector.currentWeight)/10;
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
        new_left_scale_y = starting_yPos + weightDiff;
        new_right_scale_y = starting_yPos - weightDiff;
        Debug.Log("---Left: " + new_left_scale_y);
        Debug.Log("---Right: " + new_right_scale_y);
    }
}
