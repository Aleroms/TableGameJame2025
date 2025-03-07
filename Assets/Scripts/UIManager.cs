using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI; 
public class UIManager : MonoBehaviour
{
    [SerializeField] private BlockDetector leftScale; 
    [SerializeField] private BlockDetector rightScale;
    [SerializeField] private TextMeshPro leftScaleText;
    [SerializeField] private TextMeshPro rightScaleText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI penaltyText;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI thresholdText;
    [SerializeField] private TextMeshProUGUI weightDiffText;
    [SerializeField] private GameObject warningPanel; 
    private void Update()
    {
        leftScaleText.text = leftScale.currentWeight.ToString(); 
        rightScaleText.text = rightScale.currentWeight.ToString();
        levelText.text = "Level: " + GameManager.Instance.level.ToString(); 
        penaltyText.text = "Remaining Drops: " + GameManager.Instance.penalty.ToString();
        scoreText.text = "Score: " + GameManager.Instance.score.ToString();
        warningText.text = "Warning Weight: " + GameManager.Instance.weightDiffWarningThreshold.ToString(); 
        thresholdText.text = "Game Over Weight: " + GameManager.Instance.weightDiffThreshold.ToString();
        weightDiffText.text = "CURRENT WEIGHT DIFF: " + GameManager.Instance.weightDiff.ToString(); 

        if(GameManager.Instance.warning)
        {
            EnableWarning(); 
        }
        else
        {
            DisableWarning(); 
        }
    }
    
    private void EnableWarning()
    {
        warningPanel.SetActive(true); 
    }
    private void DisableWarning()
    {
        warningPanel.SetActive(false); 
    }
}
